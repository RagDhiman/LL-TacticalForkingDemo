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
        Task<bool> UpdateUnTrackedAsync(T obj);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> DeleteAsync(T obj);
        Task<bool> SaveAsync();

    }
}
