using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsApp.Businesslogic.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<int> Create(T entity);

        Task<int> Update(T entity);

        Task<int> Delete(int id);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);
    }
}
