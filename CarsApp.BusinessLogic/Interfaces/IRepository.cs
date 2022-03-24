namespace CarsApp.Businesslogic.Interfaces;

/// <summary>
/// Represents methods for working with database.
/// </summary>
/// <typeparam name="T">Entity type.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Gets entity from database.
    /// </summary>
    /// <param name="entityId">Entity id.</param>
    /// <returns></returns>
    Task<T?> GetById(int entityId);

    /// <summary>
    /// Creates new entity in database.
    /// </summary>
    /// <param name="entity">Entity to be added to database.</param>
    void Create(T entity);

    /// <summary>
    /// Updates entity data in database.
    /// </summary>
    /// <param name="entity">Entity to be updated to database.</param>
    void Update(T entity);

    /// <summary>
    /// Deletes entity from database.
    /// </summary>
    /// <param name="entity">Entity to be removed from database.</param>
    void Delete(T entity);

    /// <summary>
    /// Gets all entities from database.
    /// </summary>
    /// <returns>Task of getting all entities from database.</returns>
    Task<List<T>> GetAll();
}