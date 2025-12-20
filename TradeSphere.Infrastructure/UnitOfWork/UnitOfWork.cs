using TradeSphere.Application.Interfaces.Repositories;
using TradeSphere.Infrastructure.Repositories.AuthRepository;

namespace TradeSphere.Infrastructure.UnitOfWork
{
    public class UnitOfWork(TradeSphereDbContext context, UserManager<AppUser> userManger, IUserRepository user) : IUnitOfWork
    {
        private readonly Dictionary<string, object> repositories = new();
        private IRefreshTokenRepository? _refreshTokenRepository;

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            var key = typeof(T).Name;
            if (!repositories.ContainsKey(key))
            {
                var repo = new GenericRepository<T>(context);
                repositories.Add(key, repo);
            }

            return (IRepository<T>)repositories[key];
        }
        public IUserRepository Users
        {
            get
            {
                if (user == null)
                    user = new UserRepository(userManger, null, null);

                return user;
            }
        }

        public IRefreshTokenRepository RefreshTokens
        {
            get
            {
                _refreshTokenRepository ??= new RefreshTokenRepository(this);
                return _refreshTokenRepository;
            }
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync() => await context.Database.BeginTransactionAsync();

        public async Task<int> CommitAsync() => await context.SaveChangesAsync();


        public void Dispose() => context.Dispose();




    }
}

