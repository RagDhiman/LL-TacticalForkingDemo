namespace Shop_BackendForFrontend_API.Data
{
    public interface IGenericRepository<T> where T : IEntity
    {
        string ResourcePath { get; set; }
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> StoreNewAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
