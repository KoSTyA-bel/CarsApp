using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using CarsApp.MongoDatabase.Settings;
using System.Threading.Channels;
using StackExchange.Redis;

namespace CarsApp.MongoDatabase.Cache;

public class EngineCache : ICache<Engine>
{
    private const string StreamName = "CarsApp";
    private const string GroupName = "Engines";
    private readonly Dictionary<int, WeakReference> _cache;
    private readonly object _locker = new();
    private readonly IRedisProducer<Engine> _redisProducer;
    private readonly IRedisConsumer<Engine> _redisConsumer;
    private readonly Channel<NameValueEntry[]> _channel;

    public EngineCache(IRedisProducer<Engine> producer, IRedisConsumer<Engine> consumer)
    {
        _cache = new();
        _channel = Channel.CreateUnbounded<NameValueEntry[]>();
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

    public async void ListenChannel()
    {
        while (true)
        {
            var handeled = _redisConsumer.GetHandledElement();

            if (!(handeled is null))
            {
                await _channel.Writer.WriteAsync(handeled);
            }
        }
    }

    public async void ListenRedisStream()
    {
        while (true)
        {
            if (await _channel.Reader.WaitToReadAsync())
            {
                var streamEntity = await _channel.Reader.ReadAsync();
                var engine = Parse(streamEntity);

                if (engine is null)
                {
                    continue;
                }

                switch (streamEntity[0].Value.ToString())
                {
                    case CacheCommandTypes.Insert:
                        SetEngineInCache(engine);
                        break;
                    case CacheCommandTypes.Delete:
                        DeleteEngineFromCache(engine.Id);
                        break;
                }
            }
        }
    }

    private Engine? Parse(NameValueEntry[] values) =>
        values.Length switch
        {
            3 => new Engine() { Id = int.Parse(values[1].Value), Name = values[2].Value },
            2 => new Engine() { Id = int.Parse(values[1].Value) },
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
}
