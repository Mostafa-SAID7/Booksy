using Booksy.Models.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booksy.DataAccess.Configurations
{
    /// <summary>
    /// EF Configuration for Tag entity
    /// Defines many-to-many relationships with Books, indices, and constraints
    /// </summary>
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.Id);

            // Many-to-Many: Tags to Books (implicit join table)
            builder.HasMany(t => t.Books)
                   .WithMany(b => b.Tags)
                   .UsingEntity(
                       "BookTag",
                       l => l.HasOne(typeof(Book)).WithMany().HasForeignKey("BookId"),
                       r => r.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagId"));

            // Indices for performance optimization
            builder.HasIndex(t => t.Name)
                   .HasDatabaseName("IX_Tag_Name")
                   .IsUnique(false);

            builder.HasIndex(t => t.Slug)
                   .HasDatabaseName("IX_Tag_Slug")
                   .IsUnique(true);

            // Soft delete index
            builder.HasIndex(t => t.IsDeleted)
                   .HasDatabaseName("IX_Tag_IsDeleted");

            // Property configurations
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(t => t.Slug)
                   .IsRequired()
                   .HasMaxLength(120);

            builder.Property(t => t.IsDeleted)
                   .HasDefaultValue(false);
        }
    }
}
