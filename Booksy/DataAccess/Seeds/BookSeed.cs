using Booksy.Models.Entities.Books;
using Microsoft.EntityFrameworkCore;

namespace Booksy.DataAccess.Seeds
{
    public static class BookSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Harry Potter and the Philosopher's Stone", ISBN = "9780747532699", Price = 19.99M, Stock = 50, AuthorId = 1, CategoryId = 5 },
                new Book { Id = 2, Title = "Harry Potter and the Chamber of Secrets", ISBN = "9780747538493", Price = 19.99M, Stock = 45, AuthorId = 1, CategoryId = 5 },
                new Book { Id = 3, Title = "A Game of Thrones", ISBN = "9780553103540", Price = 24.99M, Stock = 40, AuthorId = 2, CategoryId = 1 },
                new Book { Id = 4, Title = "A Clash of Kings", ISBN = "9780553108033", Price = 24.99M, Stock = 35, AuthorId = 2, CategoryId = 1 },
                new Book { Id = 5, Title = "The Shining", ISBN = "9780385121675", Price = 17.99M, Stock = 30, AuthorId = 5, CategoryId = 9 },
                new Book { Id = 6, Title = "It", ISBN = "9780450411434", Price = 18.99M, Stock = 25, AuthorId = 5, CategoryId = 9 },
                new Book { Id = 7, Title = "The Lord of the Rings: Fellowship", ISBN = "9780547928210", Price = 22.99M, Stock = 40, AuthorId = 3, CategoryId = 5 },
                new Book { Id = 8, Title = "Murder on the Orient Express", ISBN = "9780062073501", Price = 14.99M, Stock = 30, AuthorId = 4, CategoryId = 6 },
                new Book { Id = 9, Title = "The Da Vinci Code", ISBN = "9780307474278", Price = 16.99M, Stock = 25, AuthorId = 6, CategoryId = 7 },
                new Book { Id = 10, Title = "The Hunger Games", ISBN = "9780439023481", Price = 18.99M, Stock = 35, AuthorId = 7, CategoryId = 14 },
                new Book { Id = 11, Title = "Catching Fire", ISBN = "9780439023498", Price = 18.99M, Stock = 35, AuthorId = 7, CategoryId = 14 },
                new Book { Id = 12, Title = "Mockingjay", ISBN = "9780439023511", Price = 18.99M, Stock = 35, AuthorId = 7, CategoryId = 14 },
                new Book { Id = 13, Title = "Foundation", ISBN = "9780553293357", Price = 15.99M, Stock = 25, AuthorId = 10, CategoryId = 14 },
                new Book { Id = 14, Title = "I, Robot", ISBN = "9780553294385", Price = 15.99M, Stock = 25, AuthorId = 10, CategoryId = 14 },
                new Book { Id = 15, Title = "The Old Man and The Sea", ISBN = "9780684801223", Price = 12.99M, Stock = 20, AuthorId = 8, CategoryId = 1 },
                new Book { Id = 16, Title = "Adventures of Huckleberry Finn", ISBN = "9780486280615", Price = 11.99M, Stock = 20, AuthorId = 9, CategoryId = 1 },
            };

            modelBuilder.Entity<Book>().HasData(books);
        }
    }
}
