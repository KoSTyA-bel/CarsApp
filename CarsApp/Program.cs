using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using CarsApp.Businesslogic.Services;
using CarsApp.DataAnnotation.Contexts;
using CarsApp.DataAnnotation.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddDbContext<CarsAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));
builder.Services.AddDbContext<CarsAppContext>(options => options.UseInMemoryDatabase("CarsApp"), ServiceLifetime.Scoped);

builder.Services.AddTransient<IRepository<Engine>, EngineRepository>();
builder.Services.AddTransient<IRepository<Car>, CarRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IService<Engine>, EngineService>();
builder.Services.AddTransient<IService<Car>, CarService>();

builder.Services.AddControllers();
builder.Services.AddControllers().AddOData(options => options.Select().OrderBy().Filter());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});
builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
