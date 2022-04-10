using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using CarsApp.MongoDatabase.Settings;
using StackExchange.Redis;

namespace CarsApp.MongoDatabase.Caches.Consumers;

public class RedisConsumer : IRedisConsumer<Engine>
{
    private readonly ISubscriber _subscriber;
    private readonly CacheSettings _settings;

    public RedisConsumer(CacheSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        var mul = ConnectionMultiplexer.Connect($"{settings.Host}:{settings.Port}");
        _subscriber = mul.GetSubscriber();
    }

    public event EventHandler<string>? OnNewDataHandled;

    public void StartListen()
    {
        _subscriber.Subscribe(_settings.StreamName, (channel, value) =>
        {
            OnNewDataHandled?.Invoke(this, value);
        });
    }
}
