using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using CarsApp.MongoDatabase.Settings;
using StackExchange.Redis;

namespace CarsApp.MongoDatabase.Caches.Producers;

public class RedisProducer : IRedisProducer<Engine>
{
    private readonly IDatabase _database;
    private readonly CacheSettings _settings;

    public RedisProducer(CacheSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        var mul = ConnectionMultiplexer.Connect($"{settings.Host}:{settings.Port}");
        _database = mul.GetDatabase();
    }

    public void Delete(int entityId)
    {
        _database.StreamAdd(_settings.StreamName, new NameValueEntry[]
        {
            new NameValueEntry(CacheFieldNames.Command, CacheCommandTypes.Delete),
            new NameValueEntry(CacheFieldNames.Id, entityId),
        });
    }

    public void Insert(Engine entity)
    {
        _database.StreamAdd(_settings.StreamName, new NameValueEntry[]
        {
            new NameValueEntry(CacheFieldNames.Command, CacheCommandTypes.Insert),
            new NameValueEntry(CacheFieldNames.Id, entity.Id),
            new NameValueEntry(CacheFieldNames.Name, entity.Name),
        });
    }
}
