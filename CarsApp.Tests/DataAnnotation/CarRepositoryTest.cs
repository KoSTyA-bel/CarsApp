using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsApp.Businesslogic.Entities;
using CarsApp.DataAnnotation.Contexts;
using CarsApp.DataAnnotation.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CarsApp.Tests.DataAnnotation
{
    [TestFixture]
    public class CarRepositoryTest
    {
        private CarRepository _repository = null!;

        private static IEnumerable<(Car car, int id)> Cars()
        {
            yield return (new Car { Name = "1" }, 1);
            yield return (new Car { Name = "2" }, 2);
            yield return (new Car { Name = "3" }, 3);
            yield return (new Car { Name = "4" }, 4);
            yield return (new Car { Name = "5" }, 5);
        }

        [SetUp]
        public void SetUp()
        {
            var builder = new DbContextOptionsBuilder<CarsAppContext>();
            builder.UseInMemoryDatabase("Test");
            _repository = new(new CarsAppContext(builder.Options));
        }

        [Order(1)]
        [TestCaseSource(nameof(Cars))]
        public void AddAllCars((Car car, int id) data)
        {
            _repository.Create(data.car);
            Assert.AreEqual(data.id, data.car.Id);
        }

        [Order(2)]
        [TestCaseSource(nameof(Cars))]
        public void UpdateAllCars((Car car, int id) data)
        {
            _repository.Create(data.car);
            data.car.Name = "TEST";
            _repository.Update(data.car);
            Assert.AreEqual(data.id + Cars().Count(), data.car.Id);
            Assert.AreEqual("TEST", data.car.Name);
        }

        [Order(3)]
        [TestCaseSource(nameof(Cars))]
        public void GetAllCars((Car car, int id) data)
        {
            var cars = _repository.GetAll().GetAwaiter().GetResult();
            int count = 1;

            foreach (Car car in cars)
            {
                Assert.AreEqual(count, car.Id);
                count++;
            }
        }

        [Order(4)]
        [Test]
        public async Task DeleteAll()
        {
            for (int i = Cars().Count() * 3; i > 0; i--)
            {
                var car = await _repository.GetById(i);
                if (car != null)
                {
                    _repository.Delete(car);
                }
                Assert.Null(await _repository.GetById(i));
            }
        }
    }
}
