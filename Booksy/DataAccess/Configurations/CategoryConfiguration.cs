using Booksy.Models.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booksy.DataAccess.Configurations
{
    /// <summary>
    /// EF Configuration for Category entity
    /// Defines relationships, indices, and constraints
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            // One-to-Many: Books in this category
            builder.HasMany(c => c.Books)
                   .WithOne(b => b.Category)
                   .HasForeignKey(b => b.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indices for performance optimization
            builder.HasIndex(c => c.Name)
                   .HasDatabaseName("IX_Category_Name")
                   .IsUnique(false);

            builder.HasIndex(c => c.Slug)
                   .HasDatabaseName("IX_Category_Slug")
                   .IsUnique(true);

            // Soft delete index
            builder.HasIndex(c => c.IsDeleted)
                   .HasDatabaseName("IX_Category_IsDeleted");

            // Property configurations
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Slug)
                   .IsRequired()
                   .HasMaxLength(120);

            builder.Property(c => c.IsDeleted)
                   .HasDefaultValue(false);
        }
    }
}
