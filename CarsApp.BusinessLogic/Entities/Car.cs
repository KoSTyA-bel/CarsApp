namespace CarsApp.Businesslogic.Entities;

public class Car
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? EngineId { get; set; }

    public Engine? Engine { get; set; }
}

