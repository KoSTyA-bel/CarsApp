using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using CarsApp.DataAnnotation.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarsApp.DataAnnotation.Repositories;

public class EngineRepository : IRepository<Engine>
{
    private readonly CarsAppContext _context;

    public EngineRepository(CarsAppContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Create(Engine entity) => _context.Engines.AddAsync(entity ?? throw new ArgumentNullException(nameof(entity)));

    public void Delete(Engine entity)
    {
        _context.Remove(entity);
    }

    public Task<List<Engine>> GetAll() => _context.Engines.ToListAsync();

    public Task<Engine?> GetById(int entityId) => _context.Engines.FirstOrDefaultAsync(engine => engine.Id == entityId);

    public void Update(Engine entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _context.Attach(entity);
        }

        _context.Entry(entity).State = EntityState.Modified;
    }
}