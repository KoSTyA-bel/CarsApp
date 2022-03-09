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

    public async void Create(Engine entity) => await _context.Engines.AddAsync(entity ?? throw new ArgumentNullException(nameof(entity)));

    public async void Delete(int entityId)
    {
        var engine = await _context.Engines.FirstOrDefaultAsync(engine => engine.Id == entityId);

        if (engine != null)
        {
            _context.Remove(engine);
        }
    }

    public async Task<IEnumerable<Engine>> GetAll() => _context.Engines;

    public async Task<Engine> GetById(int entityId) => await _context.Engines.FirstOrDefaultAsync(engine => engine.Id == entityId);

    public async void Update(Engine entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var engine = await _context.Engines.FirstOrDefaultAsync(engine => engine.Id == entity.Id);

        if (engine != null)
        {
            engine.Name = entity.Name;
        }
    }
}