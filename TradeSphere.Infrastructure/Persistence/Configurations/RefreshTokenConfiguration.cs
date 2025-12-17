namespace TradeSphere.Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");
            builder.Property(r => r.Token).IsRequired().HasMaxLength(256);
            builder.Property(r => r.ExpireOn).HasColumnType("datetime").IsRequired();
            builder.Property(r => r.CreatedOn).HasColumnType("datetime").IsRequired();
            builder.HasOne(r => r.AppUser).WithMany(a => a.RefreshTokens).HasForeignKey(r => r.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
