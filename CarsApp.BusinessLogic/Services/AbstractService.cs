using CarsApp.Businesslogic.Interfaces;

namespace CarsApp.Businesslogic.Services;

/// <summary>
/// Abstract service for working with repositories.
/// </summary>
/// <typeparam name="T">Entity type.</typeparam>
public abstract class AbstractService<T> : IService<T> where T : class
{
    private readonly IRepository<T> _repository;

    /// <summary>
    /// Creates a new instance of <see cref="AbstractService{T}"/>.
    /// </summary>
    /// <param name="repository">Instance of class that implementing <see cref="IRepository{T}"/>.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="repository"/> is null.</exception>
    protected AbstractService(IRepository<T> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <inheritdoc/>
    public virtual Task<T> Create(T entity) => Task<T>.Factory.StartNew(() => CreateEntity(entity));

    /// <inheritdoc/>
    public virtual Task Delete(T entity) => Task.Factory.StartNew(() => DeleteEntity(entity));

    /// <inheritdoc/>
    public virtual Task<List<T>> GetAll() => _repository.GetAll();

    /// <inheritdoc/>
    public virtual Task<T?> GetById(int id) => _repository.GetById(id);

    /// <inheritdoc/>
    public virtual Task Update(T entity) => Task.Factory.StartNew(() => UpdateEntity(entity));

    protected T CreateEntity(T entity)
    {
        _repository.Create(entity);
        return entity;
    }

    protected void DeleteEntity(T entity)
    {
        _repository.Delete(entity);
    }

    protected void UpdateEntity(T entity)
    {
        _repository.Update(entity);
    }
}
