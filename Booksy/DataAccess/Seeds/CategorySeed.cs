using Booksy.Models.Entities.Books;
using Microsoft.EntityFrameworkCore;

namespace Booksy.DataAccess.Seeds
{
    public static class CategorySeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Fiction" },
                new Category { Id = 2, Name = "Non-Fiction" },
                new Category { Id = 3, Name = "Science" },
                new Category { Id = 4, Name = "Children" },
                new Category { Id = 5, Name = "Fantasy" },
                new Category { Id = 6, Name = "Mystery" },
                new Category { Id = 7, Name = "Thriller" },
                new Category { Id = 8, Name = "Romance" },
                new Category { Id = 9, Name = "Horror" },
                new Category { Id = 10, Name = "Biography" },
                new Category { Id = 11, Name = "Self-Help" },
                new Category { Id = 12, Name = "History" },
                new Category { Id = 13, Name = "Poetry" },
                new Category { Id = 14, Name = "Science Fiction" }
            };

            modelBuilder.Entity<Category>().HasData(categories);
        }
    }
}
