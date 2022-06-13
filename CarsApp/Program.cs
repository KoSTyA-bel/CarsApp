using CarsApp;
using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using CarsApp.Businesslogic.Services;
using CarsApp.Businesslogic.Settings;
using CarsApp.MongoDatabase.Cache;
using CarsApp.MongoDatabase.Caches.Producers;
using CarsApp.MongoDatabase.Caches.Consumers;
using CarsApp.MongoDatabase.MongoCollectionBuilders;
using CarsApp.MongoDatabase.Repositories;
using CarsApp.MongoDatabase.Settings;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration(config =>
{
    var prefis = "CARSAPP_";
    config.AddEnvironmentVariables(prefis);
});

// Add services to the container.

// Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Mongo database
builder.Services.Configure<EngineDatabaseSettings>(builder.Configuration.GetSection(nameof(EngineDatabaseSettings)));
builder.Services.Configure<CarDatabaseSettings>(builder.Configuration.GetSection(nameof(CarDatabaseSettings)));
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));
builder.Services.AddSingleton<IMongoDatabaseSettings<Engine>>(sp => sp.GetRequiredService<IOptions<EngineDatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoDatabaseSettings<Car>>(sp => sp.GetRequiredService<IOptions<CarDatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoCollectionBuilder<Engine>, EnginesCollectionBuilder>();
builder.Services.AddSingleton<IMongoCollectionBuilder<Car>, CarsCollectionBuiler>();
builder.Services.AddSingleton<CollectionBuilder<Car>, CarsCollectionBuiler>();
builder.Services.AddSingleton<CollectionBuilder<Engine>, EnginesCollectionBuilder>();
builder.Services.AddScoped<IRepository<Engine>, EngineRepositoryMongo>();
builder.Services.AddScoped<IRepository<Car>, CarRepositoryMongo>();
builder.Services.AddScoped<IService<Engine>, EngineServiceMongo>();
builder.Services.AddScoped<IService<Car>, CarServiceMongo>();
builder.Services.AddSingleton<CacheSettings>(sp => sp.GetRequiredService<IOptions<CacheSettings>>().Value);
builder.Services.AddSingleton<IRedisProducer<Engine>, RedisProducer>();
builder.Services.AddSingleton<IRedisConsumer<Engine>, RedisConsumer>();
builder.Services.AddSingleton<ICache<Engine>, EngineCache>();

// MSSQL or InMemory
// builder.Services.AddDbContext<CarsAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));
//builder.Services.AddDbContext<CarsAppContext>(options => options.UseInMemoryDatabase("CarsApp"), ServiceLifetime.Scoped);
//builder.Services.AddScoped<IRepository<Engine>, EngineRepository>();
//builder.Services.AddScoped<IRepository<Car>, CarRepository>();
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<IService<Engine>, EngineService>();
//builder.Services.AddScoped<IService<Car>, CarService>();

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

var configEngine = app.Services.GetService(typeof(IMongoDatabaseSettings<Engine>)) as EngineDatabaseSettings;
var configCar = app.Services.GetService(typeof(IMongoDatabaseSettings<Car>)) as CarDatabaseSettings;
var configRedis = app.Services.GetService(typeof(CacheSettings)) as CacheSettings;

var mongoIp = builder.Configuration.GetSection("CARSAPP_Mongo_Ip").Value;
var mongoPort = builder.Configuration.GetSection("CARSAPP_Mongo_Port").Value;
var redisIp = builder.Configuration.GetSection("CARSAPP_Redis_Ip").Value;
var redisPort = builder.Configuration.GetSection("CARSAPP_Redis_Port").Value;

if (!string.IsNullOrEmpty(mongoIp))
{
    configEngine.Ip = mongoIp;
    configCar.Ip = mongoIp;
}

if (!string.IsNullOrEmpty(redisIp))
{
    configRedis.Host = redisIp;
}

if (!string.IsNullOrEmpty(mongoPort))
{
    configEngine.Port = int.Parse(mongoPort);
    configCar.Port = configEngine.Port;
}

if (!string.IsNullOrEmpty(redisPort))
{
    configRedis.Port = redisPort;
}

var cache = app.Services.GetService<ICache<Engine>>();

cache.ListenChannel();
cache.ListenRedisChannel();

app.Run();
