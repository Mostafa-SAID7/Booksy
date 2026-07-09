using Booksy.Models.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booksy.DataAccess.Configurations
{
    /// <summary>
    /// EF Configuration for Author entity
    /// Defines relationships, indices, and constraints
    /// </summary>
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);

            // One-to-Many: Books by this author
            builder.HasMany(a => a.Books)
                   .WithOne(b => b.Author)
                   .HasForeignKey(b => b.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indices for performance optimization
            builder.HasIndex(a => a.Name)
                   .HasDatabaseName("IX_Author_Name")
                   .IsUnique(false);

            builder.HasIndex(a => a.Slug)
                   .HasDatabaseName("IX_Author_Slug")
                   .IsUnique(true);

            // Soft delete index
            builder.HasIndex(a => a.IsDeleted)
                   .HasDatabaseName("IX_Author_IsDeleted");

            // Property configurations
            builder.Property(a => a.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.Slug)
                   .IsRequired()
                   .HasMaxLength(120);

            builder.Property(a => a.Bio)
                   .IsRequired(false)
                   .HasMaxLength(1000);

            builder.Property(a => a.IsDeleted)
                   .HasDefaultValue(false);
        }
    }
}
