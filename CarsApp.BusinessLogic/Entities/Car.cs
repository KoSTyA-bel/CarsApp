namespace CarsApp.Businesslogic.Entities;

public class Car : IEquatable<Car>
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? EngineId { get; set; }

    public Engine? Engine { get; set; }

    public override bool Equals(object? obj) => Equals(obj as Car);

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

