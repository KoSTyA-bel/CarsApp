namespace CarsApp.Businesslogic.Interfaces;

/// <summary>
/// Represents properties for storing settings of database.
/// </summary>
/// <typeparam name="T">Entity type.</typeparam>
public interface IMongoDatabaseSettings<T> where T : class
{
    /// <summary>
    /// Collection name.
    /// </summary>
    string CollectionName { get; set; }

    /// <summary>
    /// Connecion string.
    /// </summary>
    string Ip { get; set; }

    /// <summary>
    /// Port.
    /// </summary>
    int Port { get; set; }

    /// <summary>
    /// Database name.
    /// </summary>
    string DatabaseName { get; set; }

    /// <summary>
    /// Admin login.
    /// </summary>
    string Login { get; set; }

    /// <summary>
    /// Admin password.
    /// </summary>
    string Password { get; set; }
}
