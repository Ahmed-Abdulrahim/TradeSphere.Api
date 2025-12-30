using TradeSphere.Application.Mapping.CategoryProfile;
using TradeSphere.Infrastructure.Repositories.CategoryRepository;
using TradeSphere.Infrastructure.Repositories.OrderRepository;
using TradeSphere.Infrastructure.Repositories.ProductRepository;
using TradeSphere.Infrastructure.Repositories.RoleRepository;
using TradeSphere.Infrastructure.Repositories.ShoppingCartRepository;

namespace TradeSphere.Api.Extensions
{
    public static class AddServices
    {
        public static IServiceCollection ApplyServices(this IServiceCollection service, IConfiguration configration)
        {
            service.AddDbContext<TradeSphereDbContext>(opt =>
            {
                opt.UseSqlServer(configration.GetConnectionString("conn1"));
            });

            service.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<TradeSphereDbContext>().AddDefaultTokenProviders(); ;

            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IAuthRepository, AuthRepository>();
            service.AddScoped<IAccountRepository, AccountRepository>();
            service.AddScoped<AuthUseCase>();
            service.AddScoped<AccountUseCase>();
            service.AddScoped<ProductUseCase>();
            service.AddScoped<CategoryUseCase>();
            service.AddScoped<OrderUseCase>();
            service.AddScoped<ShoppingCartUseCase>();
            service.AddScoped<RoleUseCase>();
            service.AddScoped<IEmailService, EmailService>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<IOrderRepository, OrderRepository>();
            service.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            service.AddScoped<IRoleRepository, RoleRepository>();
            service.AddAutoMapper(cfg => { }, typeof(CategoryProfile).Assembly);


            return service;
        }

    }
}
