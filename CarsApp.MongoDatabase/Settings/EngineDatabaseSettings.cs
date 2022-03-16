using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.Businesslogic.Settings;

public class EngineDatabaseSettings : IMongoDatabaseSettings<Engine>
{
    private string _collectionName = null!;
    private string _connectionString = null!;
    private string _databaseName = null!;
    private string _login = null!;
    private string _password = null!;

    public EngineDatabaseSettings()
        : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
    {
    }

    public EngineDatabaseSettings(string collectionName, string connectionString, string databaseName, string login, string password)
    {
        CollectionName = collectionName;
        ConnectionString = connectionString;
        DatabaseName = databaseName;
        Login = login;
        Password = password;
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

    public string Login
    {
        get => _login;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _login = value;
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _password = value;
        }
    }
}
