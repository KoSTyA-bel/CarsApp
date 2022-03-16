using CarsApp.Businesslogic.Interfaces;
using CarsApp.Businesslogic.Entities;
using MongoDB.Driver;
using CarsApp.MongoDatabase.MongoCollectionBuilders;

namespace CarsApp.MongoDatabase.Repositories;

public class EngineRepositoryMongo : IRepository<Engine>
{
    private readonly IMongoCollection<Engine> _engines;

    public EngineRepositoryMongo(IMongoCollectionBuilder<Engine> builder)
    {
        _engines = builder.GetCollection();
    }

    public void Create(Engine entity)
    {
        _engines.InsertOne(entity);
    }

    public void Delete(Engine entity)
    {
        _engines.DeleteOne(toDelete => toDelete.Id == entity.Id);
    }

    public Task<List<Engine>> GetAll() => Task<List<Engine>>.Factory.StartNew(() => _engines.Find(engine => true).ToList());

    public Task<Engine?> GetById(int entityId) => _engines.Find(engine => engine.Id == entityId).FirstAsync();

    public void Update(Engine entity)
    {
        _engines.ReplaceOne(engine => engine.Id == entity.Id, entity);
    }
}
