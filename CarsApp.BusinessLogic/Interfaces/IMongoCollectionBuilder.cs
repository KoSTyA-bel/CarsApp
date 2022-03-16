using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.Businesslogic.Interfaces;

public interface IMongoCollectionBuilder<T> where T : class
{
    public IMongoDatabaseSettings<T> Settings { get; set; }

    public IMongoCollection<T> GetCollection();
}
