namespace CarsApp.Businesslogic.Interfaces;

public interface IRedisProducer<T> where T : class
{
    public void Insert(T entity);

    public void Delete(int entityId);
}
