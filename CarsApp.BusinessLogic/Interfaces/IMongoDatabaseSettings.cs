using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.Businesslogic.Interfaces
{
    public interface IMongoDatabaseSettings<T> where T: class 
    {
        string CollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
