using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsApp.Businesslogic.Interfaces;
using CarsApp.Businesslogic.Entities;
using MongoDB.Driver;

namespace CarsApp.MongoDatabase.MongoCollectionBuilders;

public class EnginesCollectionBuilder : CollectionBuilder<Engine>
{
    public EnginesCollectionBuilder(IMongoDatabaseSettings<Engine> settings) : base(settings)
    {
    }
}
