using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;

namespace CarsApp.Businesslogic.Services;

/// <summary>
/// A specific implementation of the service for working with engines located in a Microsoft database.
/// </summary>
public class EngineService : AbstractService<Engine>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Creates a new instance of <see cref="EngineService"/>.
    /// </summary>
    /// <param name="repository">Instance of class that implementing <see cref="IRepository{T}"/>.</param>
    /// <param name="unitOfWork">Instance of class that implementing <see cref="IUnitOfWork"/>.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="unitOfWork"/> is null.</exception>
    public EngineService(IRepository<Engine> repository, IUnitOfWork unitOfWork)
        : base(repository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public override async Task<Engine> Create(Engine engine)
    {
        var entity = CreateEntity(engine);
        await _unitOfWork.Save();
        return entity;
    }

    /// <inheritdoc/>
    public override Task Delete(Engine engine)
    {
        DeleteEntity(engine);
        return _unitOfWork.Save();
    }

    /// <inheritdoc/>
    public override Task Update(Engine engine)
    {
        UpdateEntity(engine);
        return _unitOfWork.Save();
    }
}

