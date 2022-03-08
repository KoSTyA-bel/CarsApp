using CarsApp.Businesslogic.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.Tests.BuissnesLogic
{
    [TestFixture]

    public class CarTest
    {
        [TestCase("Id", typeof(int), ExpectedResult = true)]
        [TestCase("Name", typeof(string), ExpectedResult = true)]
        [TestCase("EngineId", typeof(int?), ExpectedResult = true)]
        [TestCase("Engine", typeof(Engine), ExpectedResult = true)]
        public bool IsClassContainProperty(string fieldName, Type type)
        {
            var prop = typeof(Car).GetProperty(fieldName);
            return prop.PropertyType == type;
        }
    }
}
