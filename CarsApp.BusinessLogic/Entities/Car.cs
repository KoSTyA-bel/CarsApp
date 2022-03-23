namespace CarsApp.Businesslogic.Entities;

/// <summary>
/// Class container for storing information about car.
/// </summary>
public class Car : IEquatable<Car>
{
    /// <summary>
    /// Car id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Car name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Engine id.
    /// </summary>
    public int? EngineId { get; set; }

    /// <summary>
    /// Car engine.
    /// </summary>
    public Engine? Engine { get; set; }

    /// <summary>
    /// Check if the passed object is equal to an instance.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <returns>True, if the objects are equal. False in other cases.</returns>
    public override bool Equals(object? obj) => Equals(obj as Car);

    /// <summary>
    /// Check if the passed object is equal to an instance.
    /// </summary>
    /// <param name="obj">Another car.</param>
    /// <returns>True, if the objects are equal. False in other cases.</returns>
    public bool Equals(Car? other)
    {
        if (other is null)
        {
            return false;
        }

        if (GetHashCode() != other.GetHashCode())
        {
            return false;
        }

        return Id.Equals(other.Id) && string.Equals(Name, other.Name) && EngineId.Value == other.EngineId.Value;
    }
}

