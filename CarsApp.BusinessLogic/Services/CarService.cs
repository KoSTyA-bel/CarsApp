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

    public async Task<int> Create(Car car)
    {
        _repository.Create(car);

        if (await _unitOfWork.Save() == 1)
        {
            return car.Id;
        }

        return -1;
    }

    public async Task<int> Delete(int id)
    {
        _repository.Delete(id);

        if (await _unitOfWork.Save() == 1)
        {
            return id;
        }

        return -1;
    }

    public async Task<IEnumerable<Car>> GetAll() => await _repository.GetAll();

    public async Task<Car> GetById(int id) => await _repository.GetById(id);

    public async Task<int> Update(Car car)
    {
        _repository.Update(car);

        if (await _unitOfWork.Save() == 1)
        {
            return car.Id;
        }

        return -1;
    }
}
