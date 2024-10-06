using System.Linq.Expressions;

namespace ECommerceAPI.BL.Interfaces
{
    public interface IRepo<T> where T : class
    {
        public Task<bool> CreateAsync(T entity);
        public Task<bool> UpdateAsync(T entity);
        public Task<bool> DeleteAsync(T entity);
        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        public Task<IEnumerable<T>> FindAllAsync();
        public Task<T> FindWithKeysAsync(T entity);

    }
}
