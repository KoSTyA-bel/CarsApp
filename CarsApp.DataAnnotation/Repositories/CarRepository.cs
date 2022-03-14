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

    public void Create(Car entity) => _context.Cars.AddAsync(entity ?? throw new ArgumentNullException(nameof(entity)));

    public void Delete(Car entity)
    {
        _context.Remove(entity);
    }

    public Task<List<Car>> GetAll() => _context.Cars.ToListAsync();

    public Task<Car?> GetById(int entityId) => _context.Cars.FirstOrDefaultAsync(c => c.Id == entityId);

    public void Update(Car entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _context.Attach(entity);
        }

        _context.Entry(entity).State = EntityState.Modified;
    }
}