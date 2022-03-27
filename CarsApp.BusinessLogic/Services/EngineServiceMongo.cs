using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;

namespace CarsApp.Businesslogic.Services;

/// <summary>
/// A specific implementation of the service for working with engines located in a Mongo database.
/// </summary>
public class EngineServiceMongo : AbstractService<Engine>
{
    private readonly ICache<Engine> _engineCache;

    /// <summary>
    /// Creates a new instance of <see cref="EngineServiceMongo"/>.
    /// </summary>
    /// <param name="repository">Instance of class that implementing <see cref="IRepository{T}"/>.</param>
    public EngineServiceMongo(IRepository<Engine> repository, ICache<Engine> engineCache) : base(repository)
    {
        _engineCache = engineCache ?? throw new ArgumentNullException(nameof(engineCache));
    }

    public override async Task<Engine?> GetById(int id)
    {
        Engine? engine;

        if (!((engine = _engineCache.Get(id)) is null))
        {
            return engine;
        }

        engine = await base.GetById(id);

        if (!(engine is null))
        {
            _engineCache.Set(engine);
        }

        return engine;
    }
}
