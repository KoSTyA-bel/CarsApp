using StackExchange.Redis;

namespace CarsApp.Businesslogic.Interfaces;

public interface IRedisConsumer<T> where T : class
{
    public NameValueEntry[] GetHandledElement();
}
