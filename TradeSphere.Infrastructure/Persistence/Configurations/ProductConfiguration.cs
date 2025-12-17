namespace TradeSphere.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.FeedBacks).WithOne(f => f.Product).HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.OrderItems).WithOne(o => o.Product).HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(p => p.CartItems).WithOne(c => c.Product).HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
