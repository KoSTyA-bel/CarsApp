using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using CarsApp.MongoDatabase.Settings;

namespace CarsApp.MongoDatabase.Cache;

public class EngineCache : ICache<Engine>
{
    private const string StreamName = "CarsApp";
    private const string GroupName = "Engines";
    private readonly Dictionary<int, WeakReference> _cache;
    private readonly object _locker = new();
    private readonly CacheSettings _settings;

    public EngineCache(CacheSettings settings)
    {
        _cache = new();
        _settings = settings;
    }

    public void Delete(int id)
    {
        lock (_locker)
        {
            _cache.Remove(id);
        }
    }

    public Engine? Get(int id)
    {
        lock (_locker)
        {
            if (!_cache.ContainsKey(id))
            {
                return null;
            }

            if (!_cache[id].IsAlive)
            {
                _cache.Remove(id);
                return null;
            }

            return _cache[id].Target as Engine;
        }
    }

    public void Set(Engine entity)
    {
        lock (_locker)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (Get(entity.Id) is null)
            {
                _cache.Add(entity.Id, new WeakReference(entity));
            }
        }
    }
}
