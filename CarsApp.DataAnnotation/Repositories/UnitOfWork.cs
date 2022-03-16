using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsApp.Businesslogic.Interfaces;
using CarsApp.DataAnnotation.Contexts;

namespace CarsApp.DataAnnotation.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CarsAppContext _context;

    public UnitOfWork(CarsAppContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<int> Save() => _context.SaveChangesAsync();
}
