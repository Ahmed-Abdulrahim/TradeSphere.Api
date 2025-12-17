
namespace TradeSphere.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {

        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(p => p.Amount).HasColumnType("decimal(18.2)");
            builder.Property(p => p.Status).HasConversion<string>()
                .HasDefaultValue(PaymentStatus.Pending).IsRequired();
            builder.Property(p => p.PaymentDate).HasColumnType("datetime");
            builder.HasOne(p => p.Order).WithOne(o => o.Payment).HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.AppUser).WithMany(a => a.Payments).HasForeignKey(p => p.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
