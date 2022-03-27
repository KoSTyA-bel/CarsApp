using CarsApp.Businesslogic.Interfaces;
using CarsApp.Businesslogic.Entities;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CarsApp.MongoDatabase.Cache
{
    public class EngineCache : ICache<Engine>
    {
        private Dictionary<int, WeakReference> _cache;

        public EngineCache()
        {
            _cache = new();
        }

        public void Delete(int id)
        {
            _cache.Remove(id);
        }

        public Engine? Get(int id)
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

        public void Set(Engine entity)
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
