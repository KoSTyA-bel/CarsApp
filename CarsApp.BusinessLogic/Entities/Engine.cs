namespace CarsApp.Businesslogic.Entities;

/// <summary>
/// Class container for storing information about engine.
/// </summary>
public class Engine : IEquatable<Engine>
{
    /// <summary>
    /// Engine id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Engine name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// List of the cars with this engine.
    /// </summary>
    public List<Car> Cars { get; set; }

    /// <summary>
    /// Creates a new instanse of <see cref="Engine"/>.
    /// </summary>
    public Engine()
    {
        Name = String.Empty;
        Cars = new();
    }

    public static bool operator ==(Engine left, Engine right) => left is null ? false : left.Equals(right);

    public static bool operator !=(Engine left, Engine right) => left is null ? false : !left.Equals(right);

    /// <summary>
    /// Check if the passed object is equal to an instance.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <returns>True, if the objects are equal. False in other cases.</returns>
    public override bool Equals(object? obj) => Equals(obj as Engine);

    /// <summary>
    /// Check if the passed object is equal to an instance.
    /// </summary>
    /// <param name="obj">Another engine.</param>
    /// <returns>True, if the objects are equal. False in other cases.</returns>
    public bool Equals(Engine? other)
    {
        if (other is null)
        {
            return false;
        }

        if (this.GetHashCode() != other.GetHashCode())
        {
            return false;
        }

        return Id.Equals(other.Id) && string.Equals(Name, other.Name) && Cars.Equals(other.Cars);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        int res = Id.GetHashCode();
        return res ^ Name.GetHashCode();
    }
}

