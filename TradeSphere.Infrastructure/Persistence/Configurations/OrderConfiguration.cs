namespace TradeSphere.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.OrderDate).HasColumnType("datetime");
            builder.Property(o => o.TotalAmount).HasColumnType("decimal(18.2)").IsRequired();
            builder.Property(o => o.Status).HasConversion<string>()
                .HasDefaultValue(OrderStatus.Pending).IsRequired();
            builder.HasOne(o => o.AppUser).WithMany(a => a.Orders).HasForeignKey(o => o.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(o => o.OrderItems).WithOne(o => o.Order).HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
