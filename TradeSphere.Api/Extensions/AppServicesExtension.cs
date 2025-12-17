namespace TradeSphere.Api.Extensions
{
    public static class AppServicesExtension
    {
        public static async Task ApplyMigrationWithSeed(this WebApplication app)
        {
            using var scoped = app.Services.CreateScope();
            var services = scoped.ServiceProvider;
            var dbContext = services.GetRequiredService<TradeSphereDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var env = services.GetRequiredService<IWebHostEnvironment>();

            try
            {
                await dbContext.Database.MigrateAsync();
                await dbContext.SeedDataAsync(env.ContentRootPath);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error has occurred during migration or seeding!");
            }
        }
    }
}
