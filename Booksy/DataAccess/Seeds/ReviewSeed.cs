using Booksy.Models.Entities.Books;
using Booksy.Models.Entities.Users;
using Booksy.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Booksy.DataAccess.Seeds
{
    public static class ReviewSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var reviews = new List<Review>
            {
                new Review { Id = 1, BookId = 1, UserId = "00000000-0000-0000-0000-000000000002", Rating = 5, Comment = "Loved this book!", Status = ReviewStatus.Approved },
                new Review { Id = 2, BookId = 3, UserId = "00000000-0000-0000-0000-000000000003", Rating = 4, Comment = "Great story.", Status = ReviewStatus.Approved },
                new Review { Id = 3, BookId = 5, UserId = "00000000-0000-0000-0000-000000000002", Rating = 5, Comment = "Terrifying but amazing.", Status = ReviewStatus.Approved },
                new Review { Id = 4, BookId = 10, UserId = "00000000-0000-0000-0000-000000000003", Rating = 5, Comment = "Could not put it down!", Status = ReviewStatus.Approved },
                new Review { Id = 5, BookId = 7, UserId = "00000000-0000-0000-0000-000000000002", Rating = 5, Comment = "Classic fantasy!", Status = ReviewStatus.Approved },
                new Review { Id = 6, BookId = 13, UserId = "00000000-0000-0000-0000-000000000003", Rating = 4, Comment = "Interesting sci-fi.", Status = ReviewStatus.Approved },
            };

            modelBuilder.Entity<Review>().HasData(reviews);
        }
    }
}
