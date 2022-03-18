using CarsApp.Businesslogic.Entities;
using CarsApp.Businesslogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.Businesslogic.Services;

public class EngineServiceMongo : AbstractService<Engine>
{
    public EngineServiceMongo(IRepository<Engine> repository) : base(repository)
    {
    }
}
