using TradeSphere.Application.Interfaces.Specefication;

namespace TradeSphere.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<T> GetByIdSpec(ISpecefication<T> spec);
        Task<IEnumerable<T>> GetAllWithSpec(ISpecefication<T> spec);
        void UpdateAsync(T entutty);
        void DeleteAsync(T entity);
        void DeleteRangeAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    }
}
