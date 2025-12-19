using TradeSphere.Application.Interfaces.UnitOfWork;
using TradeSphere.Infrastructure.UnitOfWork;

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
            service.AddScoped<IEmailService, EmailService>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();


            return service;
        }

    }
}
