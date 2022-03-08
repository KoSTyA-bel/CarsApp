using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Services;
using CarsApp.Controllers;
using CarsApp.DataAnnotation.Contexts;
using CarsApp.DataAnnotation.Repositories;
using CarsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.Tests.Controllers
{
    [TestFixture]
    internal class CarControllerTest
    {
        private CarController _controller = null!;

        private static IEnumerable<(Car car, int id)> Cars()
        {
            yield return (new Car { Name = "1", EngineId = 1 }, 1);
            yield return (new Car { Name = "2", EngineId = 1 }, 2);
            yield return (new Car { Name = "3", EngineId = 1 }, 3);
            yield return (new Car { Name = "4", EngineId = 1 }, 4);
            yield return (new Car { Name = "5", EngineId = 1 }, 5);
        }

        [SetUp]
        public void SetUp()
        {
            var builder = new DbContextOptionsBuilder<CarsAppContext>();
            builder.UseInMemoryDatabase("CarControllerTesr");
            var context = new CarsAppContext(builder.Options);
            var carRepository = new CarRepository(context);
            var engineRepository = new EngineRepository(context);
            var unit = new UnitOfWork(context);
            var carService = new CarService(carRepository, unit);
            var engineService = new EngineService(engineRepository, unit);
            _controller = new CarController(carService, engineService);

            var engineController = new EngineController(engineService);
            engineController.Create(new EngineViewModel() { Name = "first" }).GetAwaiter().GetResult();
        }

        [Order(1)]
        [TestCaseSource(nameof(Cars))]
        public void CreateTest((Car car, int id) data)
        {
            var result = _controller.Create(new CarViewModel() { Name = data.car.Name, EngineId = data.car.EngineId.Value }).GetAwaiter().GetResult();
            var okResult = result as OkObjectResult;
            Assert.AreEqual(data.id, (int)okResult.Value);
        }

        [Test]
        [Order(2)]
        public void GetAllTest()
        {
            var result = _controller.GetAll().GetAwaiter().GetResult();
            var okResult = result as OkObjectResult;
            var data = okResult.Value as IEnumerable<CarViewModel>;
            Assert.AreEqual(Cars().Count(), data.Count());
        }

        [Test]
        [Order(3)]
        public void GetTest()
        {
            for (int i = 1; i <= Cars().Count(); i++)
            {
                var result = _controller.Get(i).GetAwaiter().GetResult();
                var okResult = result as OkObjectResult;
                var data = okResult.Value as CarViewModel;
                Assert.NotNull(data);
                Assert.AreEqual(i, data.Id);
            }
        }

        [Test]
        [Order(4)]
        public void UpdateTest()
        {
            for (int i = 1; i <= Cars().Count(); i++)
            {
                var getResult = _controller.Get(i).GetAwaiter().GetResult();
                var okGetResult = getResult as OkObjectResult;
                var car = okGetResult.Value as CarViewModel;
                car.Name = "Test";
                var updateResult = _controller.Update(car).GetAwaiter().GetResult();
                var okUpdateResult = updateResult as OkObjectResult;
                Assert.AreEqual(i, (int)okUpdateResult.Value);
            }
        }

        [Test]
        [Order(5)]
        public void DeleteTest()
        {
            for (int i = 1; i <= Cars().Count(); i++)
            {
                var result = _controller.Delete(i).GetAwaiter().GetResult();
                var okResult = result as OkObjectResult;
                Assert.AreEqual(i, (int)okResult.Value);
            }

            var getAllResult = _controller.GetAll().GetAwaiter().GetResult();
            var okGetAllResult = getAllResult as NoContentResult;
            Assert.NotNull(okGetAllResult);
        }
    }
}
