using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using CarsApp.DataAnnotation.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarsApp.DataAnnotation.Repositories;

public class CarRepository : IRepository<Car>
{
    private readonly CarsAppContext _context;

    public CarRepository(CarsAppContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async void Create(Car entity) => await _context.Cars.AddAsync(entity ?? throw new ArgumentNullException(nameof(entity)));

    public async Task Delete(int entityId)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(car => car.Id == entityId);

        if (car != null)
        {
            _context.Cars.Remove(car);
        }
    }

    public async Task<IEnumerable<Car>> GetAll() => _context.Cars;

    public async Task<Car> GetById(int entityId) => await _context.Cars.FirstOrDefaultAsync(c => c.Id == entityId);

    public async void Update(Car entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var car = await _context.Cars.FirstOrDefaultAsync(car => car.Id == entity.Id);

        if (car != null)
        {
            car.Name = entity.Name;
            car.EngineId = entity.EngineId;
        }
    }
}