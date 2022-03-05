namespace CarsApp.Businesslogic.Entities;

public class Engine
{
    public Engine()
    {
        Cars = new();
    }

    public int Id { get; set; }

    public string? Name { get; set; }

    public List<Car> Cars { get; set; }
}

