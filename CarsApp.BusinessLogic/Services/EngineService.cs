using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;

namespace CarsApp.Businesslogic.Services;

public class EngineService : IService<Engine>
{
    private readonly IRepository<Engine> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public EngineService(IRepository<Engine> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public Task<int> Create(Engine engine)
    {
        _repository.Create(engine);
        return _unitOfWork.Save();
    }

    public Task<int> Delete(Engine engine)
    {
        _repository.Delete(engine);
        return _unitOfWork.Save();
    }

    public Task<List<Engine>> GetAll() => _repository.GetAll();

    public Task<Engine?> GetById(int id) => _repository.GetById(id);

    public Task<int> Update(Engine engine)
    {
        _repository.Update(engine);
        return _unitOfWork.Save();
    }
}

