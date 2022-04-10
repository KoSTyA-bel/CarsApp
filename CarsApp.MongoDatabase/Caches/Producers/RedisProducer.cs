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
        var messgage = $"{CacheFieldNames.Command}:{CacheCommandTypes.Delete};{CacheFieldNames.Id}:{entityId}";
        _database.Publish(_settings.StreamName, new RedisValue(messgage));
    }

    public void Insert(Engine entity)
    {
        var message = $"{CacheFieldNames.Command}:{CacheCommandTypes.Insert};{CacheFieldNames.Id}:{entity.Id};{CacheFieldNames.Name}:{entity.Name}";
        _database.Publish(_settings.StreamName, new RedisValue(message));
    }
}
