using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Repositories.Interface
{
    public interface IGenericRepository <T> where T : class
    {
        Task<IEnumerable<T>> All();

        Task<T> GetById(int Id);

        Task<bool> Add(T entity);

        Task<bool> Save();
    }
}
