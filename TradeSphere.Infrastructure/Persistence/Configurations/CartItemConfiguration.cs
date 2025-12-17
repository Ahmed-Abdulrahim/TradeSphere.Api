
namespace TradeSphere.Infrastructure.Persistence.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        void IEntityTypeConfiguration<CartItem>.Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");
            builder.HasOne(c => c.Product).WithMany(p => p.CartItems).HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(c => c.ShoppingCart).WithMany(c => c.CartItems).HasForeignKey(c => c.ShoppingCartId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(c => new { c.ShoppingCartId, c.ProductId })
                .IsUnique();

        }
    }
}
