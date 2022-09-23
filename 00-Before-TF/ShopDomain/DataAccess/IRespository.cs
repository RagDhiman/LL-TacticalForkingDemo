using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDomain.DataAccess
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        ValueTask<T?> GetByIdAsync(object id);
        Task<bool> AddAsync(T obj);
        Task<bool> UpdateAsync(T obj);
        Task<bool> Delete(object id);
    }
}
