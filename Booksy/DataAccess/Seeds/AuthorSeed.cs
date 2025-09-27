using Booksy.Models.Entities.Books;
using Microsoft.EntityFrameworkCore;

namespace Booksy.DataAccess.Seeds
{
    public static class AuthorSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var authors = new List<Author>
            {
                new Author { Id = 1, Name = "J.K. Rowling", Bio = "Author of Harry Potter" },
                new Author { Id = 2, Name = "George R.R. Martin", Bio = "Author of Game of Thrones" },
                new Author { Id = 3, Name = "J.R.R. Tolkien", Bio = "Author of The Lord of the Rings" },
                new Author { Id = 4, Name = "Agatha Christie", Bio = "Famous mystery writer" },
                new Author { Id = 5, Name = "Stephen King", Bio = "Horror and thriller author" },
                new Author { Id = 6, Name = "Dan Brown", Bio = "Author of Da Vinci Code" },
                new Author { Id = 7, Name = "Suzanne Collins", Bio = "Author of Hunger Games" },
                new Author { Id = 8, Name = "Ernest Hemingway", Bio = "American novelist" },
                new Author { Id = 9, Name = "Mark Twain", Bio = "Famous American writer" },
                new Author { Id = 10, Name = "Isaac Asimov", Bio = "Science fiction author" }
            };

            modelBuilder.Entity<Author>().HasData(authors);
        }
    }
}
