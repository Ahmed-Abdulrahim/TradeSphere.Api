namespace TradeSphere.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.Property(c => c.Name).HasColumnType("Nvarchar").HasMaxLength(50);
            builder.HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
