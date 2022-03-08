using NUnit.Framework;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsApp.Businesslogic.Entities;

namespace CarsApp.Tests.BuissnesLogic
{
    [TestFixture]
    public class EngineTest
    {
        [TestCase("Id", typeof(int), ExpectedResult = true)]
        [TestCase("Name", typeof(string), ExpectedResult = true)]
        [TestCase("Cars", typeof(List<Car>), ExpectedResult = true)]
        public bool IsClassContainProperty(string fieldName, Type type)
        {
            var prop = typeof(Engine).GetProperty(fieldName);
            return prop.PropertyType == type;
        }
    }
}
