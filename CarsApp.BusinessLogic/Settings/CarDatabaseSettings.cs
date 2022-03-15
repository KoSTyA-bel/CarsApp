using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.Businesslogic.Settings;

public class CarDatabaseSettings : IMongoDatabaseSettings<Car>
{
    private string _collectionName;
    private string _connectionString;
    private string _databaseName;

    public CarDatabaseSettings(string collectionName, string connectionString, string databaseName)
    {
        _collectionName = collectionName ?? throw new ArgumentNullException(nameof(collectionName));
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
    }

    public string CollectionName 
    { 
        get => _collectionName;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _collectionName = value;
        }
    }

    public string ConnectionString 
    { 
        get => _connectionString;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _connectionString = value;
        }
    }

    public string DatabaseName 
    { 
        get => _databaseName;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _databaseName = value;
        }
    }
}
