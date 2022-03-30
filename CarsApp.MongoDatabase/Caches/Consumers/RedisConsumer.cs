using CarsApp.Businesslogic.Interfaces;
using CarsApp.Businesslogic.Entities;
using StackExchange.Redis;
using CarsApp.MongoDatabase.Settings;

namespace CarsApp.MongoDatabase.Caches.Consumers;

public class RedisConsumer : IRedisConsumer<Engine>
{
    private readonly IDatabase _database;
    private readonly CacheSettings _settings;

    public RedisConsumer(CacheSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        var mul = ConnectionMultiplexer.Connect($"{settings.Host}:{settings.Port}");
        _database = mul.GetDatabase();
    }

    public async Task<NameValueEntry[]> GetHandledElement()
    {
        RedisResult data;

        try
        {
            data = await _database.ExecuteAsync("XREAD", "BLOCK", "4000", "STREAMS", _settings.StreamName, "$");
        }
        catch (RedisTimeoutException ex)
        {
            return null;
        }

        if (data.IsNull)
        {
            return null;
        }

        var firstLayer = ((RedisResult[])data);
        var secondLayer = ((RedisResult[])firstLayer[0]);
        var thirdLayer = ((RedisResult[])secondLayer[1]);
        var foursLayer = ((RedisResult[])thirdLayer[0]);
        var fiveLayer = ((RedisValue[])foursLayer[1]);

        var result = new NameValueEntry[]
        {
            new NameValueEntry(fiveLayer[0], fiveLayer[1]),
            new NameValueEntry(fiveLayer[2], fiveLayer[3]),
            new NameValueEntry(fiveLayer[4], fiveLayer[5]),
        };

        return result;
    }
}
