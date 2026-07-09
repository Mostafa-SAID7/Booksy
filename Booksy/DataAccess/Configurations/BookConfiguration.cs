using Booksy.Models.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booksy.DataAccess.Configurations
{
    /// <summary>
    /// EF Configuration for Book entity
    /// Defines relationships, indices, and constraints with GUID primary keys
    /// </summary>
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            // FK Relationships
            builder.HasOne(b => b.Author)
                   .WithMany(a => a.Books)
                   .HasForeignKey(b => b.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Category)
                   .WithMany(c => c.Books)
                   .HasForeignKey(b => b.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many: Reviews
            builder.HasMany(b => b.Reviews)
                   .WithOne(r => r.Book)
                   .HasForeignKey(r => r.BookId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Many-to-Many: Tags (implicit join table)
            builder.HasMany(b => b.Tags)
                   .WithMany(t => t.Books);

            // Indices for performance optimization
            // Search and pagination
            builder.HasIndex(b => b.Title)
                   .HasDatabaseName("IX_Book_Title")
                   .IsUnique(false);

            builder.HasIndex(b => b.Slug)
                   .HasDatabaseName("IX_Book_Slug")
                   .IsUnique(true);

            builder.HasIndex(b => b.ISBN)
                   .HasDatabaseName("IX_Book_ISBN")
                   .IsUnique(true);

            // FK indices for Join operations
            builder.HasIndex(b => b.CategoryId)
                   .HasDatabaseName("IX_Book_CategoryId");

            builder.HasIndex(b => b.AuthorId)
                   .HasDatabaseName("IX_Book_AuthorId");

            // Composite index for common queries (CategoryId + IsDeleted)
            builder.HasIndex(b => new { b.CategoryId, b.IsDeleted })
                   .HasDatabaseName("IX_Book_CategoryId_IsDeleted");

            // Property configurations
            builder.Property(b => b.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(b => b.Slug)
                   .IsRequired()
                   .HasMaxLength(220);

            builder.Property(b => b.ISBN)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(b => b.Price)
                   .HasPrecision(10, 2)
                   .IsRequired();

            builder.Property(b => b.Discount)
                   .HasPrecision(10, 2)
                   .HasDefaultValue(0);

            builder.Property(b => b.Stock)
                   .IsRequired();

            builder.Property(b => b.Traffic)
                   .HasDefaultValue(0);

            builder.Property(b => b.IsDeleted)
                   .HasDefaultValue(false);
        }
    }
}
