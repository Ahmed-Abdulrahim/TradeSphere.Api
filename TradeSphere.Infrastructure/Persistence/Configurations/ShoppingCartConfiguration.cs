namespace TradeSphere.Infrastructure.Persistence.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.ToTable("ShoppingCarts");
            builder.HasOne(s => s.AppUser).WithOne(a => a.ShoppingCart).HasForeignKey<ShoppingCart>(s => s.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(s => s.CartItems).WithOne(c => c.ShoppingCart).HasForeignKey(c => c.ShoppingCartId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
