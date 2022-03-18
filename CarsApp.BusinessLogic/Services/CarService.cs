using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;

namespace CarsApp.Businesslogic.Services;

public class CarService : AbstractService<Car>
{
    private readonly IUnitOfWork _unitOfWork;

    public CarService(IRepository<Car> repository, IUnitOfWork unitOfWork)
        : base(repository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public override async Task<Car> Create(Car car)
    {
        var entity = CreateEntity(car);
        await _unitOfWork.Save();
        return entity;
    }

    public override Task Delete(Car car)
    {
        DeleteEntity(car);
        return _unitOfWork.Save();
    }

    public override Task Update(Car car)
    {
        UpdateEntity(car);
        return _unitOfWork.Save();
    }
}
