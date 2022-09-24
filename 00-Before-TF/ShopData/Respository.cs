using Microsoft.EntityFrameworkCore;
using ShopDomain.DataAccess;

namespace ShopData
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ShopContext _context;
        private DbSet<T> _dbSet;
        public Repository()
        {
            this._context = new ShopContext();
            _dbSet = _context.Set<T>();
        }
        public Repository(ShopContext _context)
        {
            this._context = _context;
            _dbSet = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToArrayAsync<T>();
        }
        public async ValueTask<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<bool> AddAsync(T obj)
        {
            await _dbSet.AddAsync(obj);

            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task<bool> UpdateAsync(T obj)
        {
            _dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;

            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task<bool> Delete(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
