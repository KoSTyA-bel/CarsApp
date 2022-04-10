namespace CarsApp.Businesslogic.Interfaces;

public interface ICache<T> where T : class
{
    public void Set(T entity);

    public T? Get(int id);

    public void Delete(int id);

    public void ListenChannel();

    public void ListenRedisChannel();
}
