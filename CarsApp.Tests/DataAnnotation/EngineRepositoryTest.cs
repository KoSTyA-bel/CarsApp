using CarsApp.Businesslogic.Entities;
using CarsApp.DataAnnotation.Contexts;
using CarsApp.DataAnnotation.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.Tests.DataAnnotation
{
    [TestFixture]
    public class EngineRepositoryTest
    {
        private EngineRepository _repository = null!;

        private static IEnumerable<(Engine engine, int id)> Engines()
        {
            yield return (new Engine { Name = "1" }, 1);
            yield return (new Engine { Name = "2" }, 2);
            yield return (new Engine { Name = "3" }, 3);
            yield return (new Engine { Name = "4" }, 4);
            yield return (new Engine { Name = "5" }, 5);
        }

        [SetUp]
        public void SetUp()
        {
            var builder = new DbContextOptionsBuilder<CarsAppContext>();
            builder.UseInMemoryDatabase("Test");
            _repository = new(new CarsAppContext(builder.Options));
        }

        [Order(1)]
        [TestCaseSource(nameof(Engines))]
        public void AddAllCars((Engine engine, int id) data)
        {
            _repository.Create(data.engine);
            Assert.AreEqual(data.id, data.engine.Id);
        }

        [Order(2)]
        [TestCaseSource(nameof(Engines))]
        public void UpdateAllCars((Engine engine, int id) data)
        {
            _repository.Create(data.engine);
            data.engine.Name = "TEST";
            _repository.Update(data.engine);
            Assert.AreEqual(data.id + Engines().Count(), data.engine.Id);
            Assert.AreEqual("TEST", data.engine.Name);
        }

        [Order(3)]
        [TestCaseSource(nameof(Engines))]
        public void GetAllCars((Engine engine, int id) data)
        {
            var engines = _repository.GetAll().GetAwaiter().GetResult();
            int count = 1;

            foreach (Engine engine in engines)
            {
                Assert.AreEqual(count, engine.Id);
                count++;
            }
        }

        [Order(4)]
        [Test]
        public async Task DeleteAll()
        {
            for (int i = Engines().Count() * 3; i > 0; i--)
            {
                _repository.Delete(i);
                Assert.AreEqual(null, await _repository.GetById(i));
            }
        }
    }
}
