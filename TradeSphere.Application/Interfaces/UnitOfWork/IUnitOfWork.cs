using Microsoft.EntityFrameworkCore.Storage;

namespace TradeSphere.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : BaseEntity;
        IUserRepository Users { get; }
        Task<int> CommitAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
