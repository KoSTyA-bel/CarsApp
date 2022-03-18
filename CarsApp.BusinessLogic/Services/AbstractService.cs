using CarsApp.Businesslogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.Businesslogic.Services;

public abstract class AbstractService<T> : IService<T> where T : class
{
    private readonly IRepository<T> _repository;

    protected AbstractService(IRepository<T> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public virtual Task<T> Create(T entity) => Task<T>.Factory.StartNew(() => CreateEntity(entity));

    public virtual Task Delete(T entity) => Task.Factory.StartNew(() => DeleteEntity(entity));

    public virtual Task<List<T>> GetAll() => _repository.GetAll();

    public virtual Task<T?> GetById(int id) => _repository.GetById(id);

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
