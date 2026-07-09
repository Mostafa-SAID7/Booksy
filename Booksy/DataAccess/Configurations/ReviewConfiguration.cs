using Booksy.Models.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booksy.DataAccess.Configurations
{
    /// <summary>
    /// EF Configuration for Review entity
    /// Defines relationships, indices, and constraints with GUID primary keys
    /// </summary>
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            // Many-to-One: Book (GUID FK)
            builder.HasOne(r => r.Book)
                   .WithMany(b => b.Reviews)
                   .HasForeignKey(r => r.BookId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Many-to-One: User (ApplicationUser)
            builder.HasOne(r => r.User)
                   .WithMany()
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indices for performance optimization
            builder.HasIndex(r => r.BookId)
                   .HasDatabaseName("IX_Review_BookId");

            builder.HasIndex(r => r.UserId)
                   .HasDatabaseName("IX_Review_UserId");

            builder.HasIndex(r => r.Status)
                   .HasDatabaseName("IX_Review_Status");

            // Composite index: BookId + Status (common query pattern)
            builder.HasIndex(r => new { r.BookId, r.Status })
                   .HasDatabaseName("IX_Review_BookId_Status");

            // Soft delete index
            builder.HasIndex(r => r.IsDeleted)
                   .HasDatabaseName("IX_Review_IsDeleted");

            // Property configurations
            builder.Property(r => r.BookId)
                   .IsRequired();

            builder.Property(r => r.UserId)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(r => r.Rating)
                   .IsRequired();

            builder.Property(r => r.Comment)
                   .IsRequired(false)
                   .HasMaxLength(1000);

            builder.Property(r => r.Status)
                   .IsRequired()
                   .HasDefaultValue(ReviewStatus.Pending);

            builder.Property(r => r.IsDeleted)
                   .HasDefaultValue(false);
        }
    }

    public enum ReviewStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }
}
