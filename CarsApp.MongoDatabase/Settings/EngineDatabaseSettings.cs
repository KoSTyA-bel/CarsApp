using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;

namespace CarsApp.Businesslogic.Settings;

public class EngineDatabaseSettings : IMongoDatabaseSettings<Engine>
{
    private string _collectionName = null!;
    private string _ip = null!;
    private int _port;
    private string _databaseName = null!;
    private string _login = null!;
    private string _password = null!;

    public EngineDatabaseSettings()
        : this(string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty)
    {
    }

    public EngineDatabaseSettings(string collectionName, string ip, int port, string databaseName, string login, string password)
    {
        CollectionName = collectionName;
        Ip = ip;
        Port = port;
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

    public string Ip
    {
        get => _ip;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _ip = value;
        }
    }

    public int Port
    {
        get => _port;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException(nameof(value));
            }

            _port = value;
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
