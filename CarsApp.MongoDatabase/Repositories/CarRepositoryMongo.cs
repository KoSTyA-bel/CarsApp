using CarsApp.Businesslogic.Interfaces;
using CarsApp.Businesslogic.Entities;
using MongoDB.Driver;
using CarsApp.MongoDatabase.MongoCollectionBuilders;

namespace CarsApp.MongoDatabase.Repositories;

public class CarRepositoryMongo : IRepository<Car>
{
    private readonly IMongoCollection<Car> _cars;

    public CarRepositoryMongo(IMongoCollectionBuilder<Car> builder)
    {
        _cars = builder.GetCollection();
    }

    public void Create(Car entity)
    {
        _cars.InsertOne(entity);
    }

    public void Delete(Car entity)
    {
        _cars.DeleteOne(toDelete => toDelete.Id == entity.Id);
    }

    public Task<List<Car>> GetAll() => Task<List<Car>>.Factory.StartNew(() => _cars.Find(car => true).ToList());

    public Task<Car?> GetById(int entityId) => _cars.Find(car => car.Id == entityId).FirstAsync();

    public void Update(Car entity)
    {
        _cars.ReplaceOne(car => car.Id == entity.Id, entity);
    }
}
