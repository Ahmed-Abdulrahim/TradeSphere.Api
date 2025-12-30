using Microsoft.EntityFrameworkCore.Storage;
using TradeSphere.Application.Interfaces.Repositories;

namespace TradeSphere.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : BaseEntity;
        IAuthRepository Users { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        Task<int> CommitAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}

