using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.Businesslogic.Services
{
    public class EngineServiceMongo
    {
        private readonly IMongoCollection<Engine> _engines;

        public EngineServiceMongo(IMongoCollectionBuilder<Engine> builder)
        {
            _engines = builder.GetCollection();
        }

        public void Create(Engine engine)
        {
            _engines.InsertOne(engine);
        }

        public List<Engine> Get() => _engines.Find(engine => true).ToList();
    }
}
