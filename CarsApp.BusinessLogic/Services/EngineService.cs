using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;

namespace CarsApp.Businesslogic.Services;

public class EngineService : AbstractService<Engine>
{
    private readonly IUnitOfWork _unitOfWork;

    public EngineService(IRepository<Engine> repository, IUnitOfWork unitOfWork)
        : base(repository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public override async Task<Engine> Create(Engine engine)
    {
        var entity = CreateEntity(engine);
        await _unitOfWork.Save();
        return entity;
    }

    public override Task Delete(Engine engine)
    {
        DeleteEntity(engine);
        return _unitOfWork.Save();
    }

    public override Task Update(Engine engine)
    {
        UpdateEntity(engine);
        return _unitOfWork.Save();
    }
}

