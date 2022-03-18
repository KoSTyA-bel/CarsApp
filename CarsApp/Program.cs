using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using CarsApp.Businesslogic.Services;
using CarsApp.DataAnnotation.Contexts;
using CarsApp.DataAnnotation.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData;
using CarsApp;
using CarsApp.Businesslogic.Settings;
using Microsoft.Extensions.Options;
using CarsApp.MongoDatabase.MongoCollectionBuilders;
using CarsApp.MongoDatabase.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Mongo database
//builder.Services.Configure<EngineDatabaseSettings>(builder.Configuration.GetSection(nameof(EngineDatabaseSettings)));
//builder.Services.Configure<CarDatabaseSettings>(builder.Configuration.GetSection(nameof(CarDatabaseSettings)));
//builder.Services.AddSingleton<IMongoDatabaseSettings<Engine>>(sp => sp.GetRequiredService<IOptions<EngineDatabaseSettings>>().Value);
//builder.Services.AddSingleton<IMongoDatabaseSettings<Car>>(sp => sp.GetRequiredService<IOptions<CarDatabaseSettings>>().Value);
//builder.Services.AddSingleton<IMongoCollectionBuilder<Engine>, EnginesCollectionBuilder>();
//builder.Services.AddSingleton<IMongoCollectionBuilder<Car>, CarsCollectionBuiler>();
//builder.Services.AddSingleton<CollectionBuilder<Car>, CarsCollectionBuiler>();
//builder.Services.AddSingleton<CollectionBuilder<Engine>, EnginesCollectionBuilder>();
//builder.Services.AddScoped<IRepository<Engine>, EngineRepositoryMongo>();
//builder.Services.AddScoped<IRepository<Car>, CarRepositoryMongo>();
//builder.Services.AddScoped<IService<Engine>, EngineServiceMongo>();
//builder.Services.AddScoped<IService<Car>, CarServiceMongo>();

// MSSQL or InMemory
// builder.Services.AddDbContext<CarsAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));
builder.Services.AddDbContext<CarsAppContext>(options => options.UseInMemoryDatabase("CarsApp"), ServiceLifetime.Scoped);
builder.Services.AddScoped<IRepository<Engine>, EngineRepository>();
builder.Services.AddScoped<IRepository<Car>, CarRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IService<Engine>, EngineService>();
builder.Services.AddScoped<IService<Car>, CarService>();

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
