
namespace TradeSphere.Infrastructure.Persistence.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");
            builder.Property(n => n.Title).HasColumnType("Nvarchar").HasMaxLength(50).IsRequired();
            builder.Property(n => n.Message).HasColumnType("Nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(n => n.CreatedAt).HasColumnType("datetime");
            builder.HasOne(n => n.AppUser).WithMany(a => a.Notifications).HasForeignKey(n => n.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
