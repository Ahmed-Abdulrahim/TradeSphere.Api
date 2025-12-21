using TradeSphere.Application.Mapping.CategoryProfile;
using TradeSphere.Infrastructure.Repositories.CategoryRepository;
using TradeSphere.Infrastructure.Repositories.ProductRepository;

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
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<AuthUseCase>();
            service.AddScoped<ProductUseCase>();
            service.AddScoped<CategoryUseCase>();
            service.AddScoped<IEmailService, EmailService>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddAutoMapper(cfg => { }, typeof(CategoryProfile).Assembly);


            return service;
        }

    }
}
