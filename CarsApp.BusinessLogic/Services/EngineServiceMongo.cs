using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;

namespace CarsApp.Businesslogic.Services;

/// <summary>
/// A specific implementation of the service for working with engines located in a Mongo database.
/// </summary>
public class EngineServiceMongo : AbstractService<Engine>
{
    /// <summary>
    /// Creates a new instance of <see cref="EngineServiceMongo"/>.
    /// </summary>
    /// <param name="repository">Instance of class that implementing <see cref="IRepository{T}"/>.</param>
    public EngineServiceMongo(IRepository<Engine> repository) : base(repository)
    {
    }
}
