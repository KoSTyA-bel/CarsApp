using MongoDB.Driver;

namespace CarsApp.Businesslogic.Interfaces;

/// <summary>
/// Represents methods for creating <see cref="IMongoCollection{TDocument}"/>.
/// </summary>
/// <typeparam name="T">Entity type.</typeparam>
public interface IMongoCollectionBuilder<T> where T : class
{
    /// <summary>
    /// Gets or sets database settings.
    /// </summary>
    public IMongoDatabaseSettings<T> Settings { get; set; }

    /// <summary>
    /// Takes collection from database.
    /// </summary>
    /// <returns>Mongo collection.</returns>
    public IMongoCollection<T> GetCollection();
}
