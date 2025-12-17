using TradeSphere.Domain.Models.IdentityUser;

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
            }).AddEntityFrameworkStores<TradeSphereDbContext>();

            return service;
        }

    }
}
