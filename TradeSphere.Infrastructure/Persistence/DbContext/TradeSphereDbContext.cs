namespace TradeSphere.Infrastructure.Persistence.DbContext
{
    public class TradeSphereDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public TradeSphereDbContext(DbContextOptions<TradeSphereDbContext> options) : base(options)
        {

        }

        #region Db Tables
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(TradeSphereDbContext).Assembly);
        }
    }
}
