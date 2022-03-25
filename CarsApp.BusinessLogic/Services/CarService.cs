using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;

namespace CarsApp.Businesslogic.Services;

/// <summary>
/// A specific implementation of the service for working with cars located in a Microsoft database.
/// </summary>
public class CarService : AbstractService<Car>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Creates a new instance of <see cref="CarService"/>.
    /// </summary>
    /// <param name="repository">Instance of class that implementing <see cref="IRepository{T}"/>.</param>
    /// <param name="unitOfWork">Instance of class that implementing <see cref="IUnitOfWork"/>.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="unitOfWork"/> is null.</exception>
    public CarService(IRepository<Car> repository, IUnitOfWork unitOfWork)
        : base(repository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public override async Task<Car> Create(Car car)
    {
        var entity = CreateEntity(car);
        await _unitOfWork.Save();
        return entity;
    }

    /// <inheritdoc/>
    public override Task Delete(Car car)
    {
        DeleteEntity(car);
        return _unitOfWork.Save();
    }

    /// <inheritdoc/>
    public override Task Update(Car car)
    {
        UpdateEntity(car);
        return _unitOfWork.Save();
    }
}
