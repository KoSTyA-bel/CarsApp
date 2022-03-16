using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsApp.Businesslogic.Interfaces;
using CarsApp.Businesslogic.Entities;
using MongoDB.Driver;

namespace CarsApp.MongoDatabase.MongoCollectionBuilders;

public abstract class CollectionBuilder<T> : IMongoCollectionBuilder<T> where T : class
{
    private IMongoDatabaseSettings<T> _settings = null!;

    protected CollectionBuilder(IMongoDatabaseSettings<T> settings)
    {
        Settings = _settings;
    }

    public IMongoDatabaseSettings<T> Settings 
    { 
        get => _settings;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _settings = value;
        } 
    }

    public IMongoCollection<T> GetCollection()
    {
        var client = new MongoClient(_settings.ConnectionString);
        var database = client.GetDatabase(_settings.DatabaseName);

        return database.GetCollection<T>(_settings.DatabaseName);
    }
}
