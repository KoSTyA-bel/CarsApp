using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.Businesslogic.Services;

public class CarServiceMongo : AbstractService<Car>
{
    public CarServiceMongo(IRepository<Car> repository) : base(repository)
    {
    }
}
