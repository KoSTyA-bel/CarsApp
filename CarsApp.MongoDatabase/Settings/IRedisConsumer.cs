namespace CarsApp.MongoDatabase.Settings;

public interface IRedisConsumer<T> where T : class
{
    public T GetHandledElement();
}
