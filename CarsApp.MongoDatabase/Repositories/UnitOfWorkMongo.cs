using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using CarsApp.MongoDatabase.MongoCollectionBuilders;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.MongoDatabase.Repositories;

public class UnitOfWorkMongo : IUnitOfWork
{
    private readonly IMongoCollection<Engine> _engines;
    private readonly IMongoCollection<Car> _cars;

    public UnitOfWorkMongo(CollectionBuilder<Engine> engines, CollectionBuilder<Car> cars)
    {
        _engines = engines.GetCollection();
        _cars = cars.GetCollection();
    }

    public Task<int> Save() => Task<int>.Factory.StartNew(() => 1); // DO NOT WORK ==> _engines.Watch().Any() || _cars.Watch().Any() ? 1 : 0;
}
