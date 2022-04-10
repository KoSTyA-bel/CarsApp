using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using CarsApp.MongoDatabase.Settings;
using System.Threading.Channels;
using StackExchange.Redis;
using CarsApp.MongoDatabase.Caches;

namespace CarsApp.MongoDatabase.Cache;

public class EngineCache : ICache<Engine>
{
    private const string StreamName = "CarsApp";
    private const string GroupName = "Engines";
    private readonly Dictionary<int, WeakReference> _cache;
    private readonly object _locker = new();
    private readonly IRedisProducer<Engine> _redisProducer;
    private readonly IRedisConsumer<Engine> _redisConsumer;
    private readonly Channel<EngineStreamModel> _channel;

    public EngineCache(IRedisProducer<Engine> producer, IRedisConsumer<Engine> consumer)
    {
        _cache = new();
        _channel = Channel.CreateUnbounded<EngineStreamModel>();
        _redisProducer = producer ?? throw new ArgumentNullException(nameof(producer));
        _redisConsumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
    }

    public void Delete(int id)
    {
        _redisProducer.Delete(id);
    }

    public Engine? Get(int id)
    {
        lock (_locker)
        {
            if (!_cache.ContainsKey(id))
            {
                return null;
            }

            if (!_cache[id].IsAlive)
            {
                _cache.Remove(id);
                return null;
            }

            return _cache[id].Target as Engine;
        }
    }

    public void Set(Engine entity)
    {
        _redisProducer.Insert(entity);
    }

    public async void ListenRedisChannel()
    {
        _redisConsumer.OnNewDataHandled += ParseMessage;

        _redisConsumer.StartListen();
    }

    public async void ListenChannel()
    {
        while (true)
        {
            if (await _channel.Reader.WaitToReadAsync())
            {
                var streamEntity = await _channel.Reader.ReadAsync();

                if (streamEntity is null)
                {
                    break;
                }

                switch (streamEntity.Command)
                {
                    case CacheCommandTypes.Insert:
                        SetEngineInCache(new Engine { Id = streamEntity.Id, Name = streamEntity.Name });
                        break;
                    case CacheCommandTypes.Delete:
                        DeleteEngineFromCache(streamEntity.Id);
                        break;
                }
            }
        }
    }

    private EngineStreamModel? Parse(IDictionary<string, string> values) =>
        values.Count switch
        {
            3 => new EngineStreamModel() { Command = values[CacheFieldNames.Command], Id = int.Parse(values[CacheFieldNames.Id]), Name = values[CacheFieldNames.Name] },
            2 => new EngineStreamModel() { Command = values[CacheFieldNames.Command], Id = int.Parse(values[CacheFieldNames.Id]) },
            _ => null,
        };

    private void DeleteEngineFromCache(int id)
    {
        lock (_locker)
        {
            _cache.Remove(id);
        }
    }

    private void SetEngineInCache(Engine engine)
    {
        lock (_locker)
        {
            if (engine is null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            if (Get(engine.Id) is null)
            {
                _cache.Add(engine.Id, new WeakReference(engine));
            }
        }
    }

    private void ParseMessage(object sender, string message)
    {
        var dict = message.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
               .Select(part => part.Split(':'))
               .ToDictionary(split => split[0], split => split[1]);
    }
}
