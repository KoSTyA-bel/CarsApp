using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;

namespace CarsApp.Businesslogic.Services;

/// <summary>
/// A specific implementation of the service for working with cars located in a Mongo database.
/// </summary>
public class CarServiceMongo : AbstractService<Car>
{
    /// <summary>
    /// Creates a new instance of <see cref="CarServiceMongo"/>.
    /// </summary>
    /// <param name="repository">Instance of class that implementing <see cref="IRepository{T}"/>.</param>
    public CarServiceMongo(IRepository<Car> repository) : base(repository)
    {
    }
}
