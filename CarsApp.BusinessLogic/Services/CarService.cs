using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;

namespace CarsApp.Businesslogic.Services;

public class CarService : IService<Car>
{
    private readonly IRepository<Car> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CarService(IRepository<Car> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public Task<int> Create(Car car)
    {
        _repository.Create(car);
        return _unitOfWork.Save();
    }

    public Task<int> Delete(Car car)
    {
        _repository.Delete(car);
        return _unitOfWork.Save();
    }

    public Task<List<Car>> GetAll() => _repository.GetAll();

    public Task<Car?> GetById(int id) => _repository.GetById(id);

    public Task<int> Update(Car car)
    {
        _repository.Update(car);
        return _unitOfWork.Save();
    }
}
