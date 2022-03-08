using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Services;
using CarsApp.Controllers;
using CarsApp.DataAnnotation.Contexts;
using CarsApp.DataAnnotation.Repositories;
using CarsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CarsApp.Tests.Controllers
{
    [TestFixture]
    public class EngineControllerTest
    {
        private EngineController _controller = null!;
        private EngineRepository _engineRepository = null!;

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
            builder.UseInMemoryDatabase("EngineControllerTesr");
            var context = new CarsAppContext(builder.Options);
            _engineRepository = new EngineRepository(context);
            var unit = new UnitOfWork(context);
            var service = new EngineService(_engineRepository, unit);
            _controller = new EngineController(service);
        }

        [Order(1)]
        [TestCaseSource(nameof(Engines))]
        public void CreateTest((Engine engine, int id) data)
        {
            var result = _controller.Create(new EngineViewModel() { Name = data.engine.Name }).GetAwaiter().GetResult();
            var okResult = result as OkObjectResult;
            Assert.AreEqual(data.id, (int)okResult.Value);
        }

        [Test]
        [Order(2)]
        public void GetAllTest()
        {
            var result = _controller.GetAll().GetAwaiter().GetResult();
            var okResult = result as OkObjectResult;
            var data = okResult.Value as IEnumerable<EngineViewModel>;
            Assert.AreEqual(Engines().Count(), data.Count());
        }

        [Test]
        [Order(3)]
        public void GetTest()
        {
            for (int i = 1; i <= Engines().Count(); i++)
            {
                var result = _controller.Get(i).GetAwaiter().GetResult();
                var okResult = result as OkObjectResult;
                var data = okResult.Value as EngineViewModel;
                Assert.NotNull(data);
                Assert.AreEqual(i, data.Id);
            }
        } 

        [Test]
        [Order(4)]
        public void UpdateTest()
        {
            for (int i = 1; i <= Engines().Count(); i++)
            {
                var getResult = _controller.Get(i).GetAwaiter().GetResult();
                var okGetResult = getResult as OkObjectResult;
                var engine = okGetResult.Value as EngineViewModel;
                engine.Name = "Test";
                var updateResult = _controller.Update(engine).GetAwaiter().GetResult();
                var okUpdateResult = updateResult as OkObjectResult;
                Assert.AreEqual(i, (int)okUpdateResult.Value);
            }
        }

        [Test]
        [Order(5)]
        public void DeleteTest()
        {
            for (int i = 1; i <= Engines().Count(); i++)
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
