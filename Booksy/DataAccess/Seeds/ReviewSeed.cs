using Booksy.Models.Entities.Books;
using Booksy.Models.Entities.Users;
using Booksy.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Booksy.DataAccess.Seeds
{
    /// <summary>
    /// Seeds sample review data for testing
    /// All reviews reference seeded books, categories, and users
    /// All IDs are deterministic GUIDs for consistent FK relationships
    /// </summary>
    public static class ReviewSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var reviews = new List<Review>
            {
                new Review 
                { 
                    Id = Guid.Parse("40000000-0000-0000-0000-000000000001"),
                    BookId = Guid.Parse("30000000-0000-0000-0000-000000000001"),
                    UserId = "00000000-0000-0000-0000-000000000002", 
                    Rating = 5, 
                    Comment = "Absolutely loved this book! A must-read for everyone.",
                    Status = ReviewStatus.Approved,
                    ReviewerName = "Alice"
                },
                new Review 
                { 
                    Id = Guid.Parse("40000000-0000-0000-0000-000000000002"),
                    BookId = Guid.Parse("30000000-0000-0000-0000-000000000003"),
                    UserId = "00000000-0000-0000-0000-000000000003", 
                    Rating = 4, 
                    Comment = "Great story and world-building, though a bit lengthy.",
                    Status = ReviewStatus.Approved,
                    ReviewerName = "Bob"
                },
                new Review 
                { 
                    Id = Guid.Parse("40000000-0000-0000-0000-000000000003"),
                    BookId = Guid.Parse("30000000-0000-0000-0000-000000000005"),
                    UserId = "00000000-0000-0000-0000-000000000002", 
                    Rating = 5, 
                    Comment = "Terrifying but amazing. Couldn't put it down!",
                    Status = ReviewStatus.Approved,
                    ReviewerName = "Alice"
                },
                new Review 
                { 
                    Id = Guid.Parse("40000000-0000-0000-0000-000000000004"),
                    BookId = Guid.Parse("30000000-0000-0000-0000-000000000010"),
                    UserId = "00000000-0000-0000-0000-000000000003", 
                    Rating = 5, 
                    Comment = "The most thrilling read I've had all year!",
                    Status = ReviewStatus.Approved,
                    ReviewerName = "Bob"
                },
                new Review 
                { 
                    Id = Guid.Parse("40000000-0000-0000-0000-000000000005"),
                    BookId = Guid.Parse("30000000-0000-0000-0000-000000000007"),
                    UserId = "00000000-0000-0000-0000-000000000002", 
                    Rating = 5, 
                    Comment = "Classic fantasy at its finest!",
                    Status = ReviewStatus.Approved,
                    ReviewerName = "Alice"
                },
                new Review 
                { 
                    Id = Guid.Parse("40000000-0000-0000-0000-000000000006"),
                    BookId = Guid.Parse("30000000-0000-0000-0000-000000000013"),
                    UserId = "00000000-0000-0000-0000-000000000003", 
                    Rating = 4, 
                    Comment = "Interesting sci-fi concepts, great foundation for thought.",
                    Status = ReviewStatus.Approved,
                    ReviewerName = "Bob"
                },
            };

            modelBuilder.Entity<Review>().HasData(reviews);
        }
    }
}
