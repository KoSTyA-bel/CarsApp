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

    public async Task<int> Create(Engine engine)
    {
        _repository.Create(engine);

        if (await _unitOfWork.Save() == 1)
        {
            return engine.Id;
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

    public async Task<IEnumerable<Engine>> GetAll() => await _repository.GetAll();

    public Task<Engine> GetById(int id) => _repository.GetById(id);

    public async Task<int> Update(Engine engine)
    {
        _repository.Update(engine);

        if (await _unitOfWork.Save() == 1)
        {
            return engine.Id;
        }

        return -1;
    }
}

