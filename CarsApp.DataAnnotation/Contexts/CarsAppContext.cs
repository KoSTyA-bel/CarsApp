using CarsApp.Businesslogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsApp.DataAnnotation.Contexts;

public class CarsAppContext : DbContext
{
    public CarsAppContext(DbContextOptions<CarsAppContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Car> Cars { get; set; } = null!;

    public DbSet<Engine> Engines { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>().HasKey(car => car.Id);
        modelBuilder.Entity<Car>().HasOne(car => car.Engine).WithMany(engine => engine.Cars).HasForeignKey(car => car.EngineId);

        modelBuilder.Entity<Engine>().HasKey(engine => engine.Id);

        base.OnModelCreating(modelBuilder);
    }
}

