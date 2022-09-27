using Shop_UI_Web.Data.DTOs;

namespace Shop_UI_Web.Data.HTTPRepository
{
    public interface IHTTPRepository<T> where T : IEntity
    {
        string APIPath { get; set; }
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
