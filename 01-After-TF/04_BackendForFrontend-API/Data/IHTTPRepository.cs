
using Shop_BackendForFrontend_API.BaseAddresses;

namespace Shop_BackendForFrontend_API.Data
{
    public interface IHTTPRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        public void SetBaseAddress(IBaseAddress baseAddress, string APIPath);

    }
}
