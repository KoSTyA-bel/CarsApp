namespace CarsApp.Businesslogic.Entities;

public class Engine : IEquatable<Engine>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Car> Cars { get; set; }

    public Engine()
    {
        Name = String.Empty;
        Cars = new();
    }

    public static bool operator ==(Engine left, Engine right) => left is null ? false : left.Equals(right);

    public static bool operator !=(Engine left, Engine right) => left is null ? false : !left.Equals(right);

    public override bool Equals(object? obj) => Equals(obj as Engine);

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

    public override int GetHashCode()
    {
        int res = Id.GetHashCode();
        return res ^ Name.GetHashCode();
    }
}

