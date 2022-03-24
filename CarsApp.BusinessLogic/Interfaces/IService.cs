namespace CarsApp.Businesslogic.Interfaces;

/// <summary>
/// Provides methods for working with service.
/// </summary>
/// <typeparam name="T">Entity type.</typeparam>
public interface IService<T> where T : class
{
    /// <summary>
    /// Creates a new entity in repository.
    /// </summary>
    /// <param name="entity">Entity to be added to repository.</param>
    /// <returns>Task of creating an entity.</returns>
    Task<T> Create(T entity);

    /// <summary>
    /// Updates entity data in repository.
    /// </summary>
    /// <param name="entity">Entity whose data needs to be updated in repository.</param>
    /// <returns>Task to update entity.</returns>
    Task Update(T entity);

    /// <summary>
    /// Removes entity from repository.
    /// </summary>
    /// <param name="entity">Entity to be deleted from repository.</param>
    /// <returns>Task of deleting an entity.</returns>
    Task Delete(T entity);

    /// <summary>
    /// Retrieves all entities located in repository.
    /// </summary>
    /// <returns>List of all entities.</returns>
    Task<List<T>> GetAll();

    /// <summary>
    /// Searches for an entity with a specific Id in repository.
    /// </summary>
    /// <param name="id">Id of entity being searched for.</param>
    /// <returns>Entity if an entity with the passed id exists, null in other cases.</returns>
    Task<T?> GetById(int id);
}
