namespace CarsApp.Businesslogic.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetById(int entityId);

    void Create(T entity);

    void Update(T entity);

    Task Delete(int entityId);

    Task<IEnumerable<T>> GetAll();
}