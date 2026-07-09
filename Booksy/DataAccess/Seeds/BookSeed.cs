using Booksy.Models.Entities.Books;
using Booksy.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Booksy.DataAccess.Seeds
{
    /// <summary>
    /// Seeds default application books
    /// All IDs are deterministic GUIDs for consistent FK relationships
    /// </summary>
    public static class BookSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var books = new List<Book>
            {
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000001"),
                    Title = "Harry Potter and the Philosopher's Stone", 
                    ISBN = "9780747532699", 
                    Price = 19.99M, 
                    Stock = 50, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000005"),
                    Slug = SlugGenerator.Generate("Harry Potter and the Philosopher's Stone")
                },
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000002"),
                    Title = "Harry Potter and the Chamber of Secrets", 
                    ISBN = "9780747538493", 
                    Price = 19.99M, 
                    Stock = 45, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000005"),
                    Slug = SlugGenerator.Generate("Harry Potter and the Chamber of Secrets")
                },
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000003"),
                    Title = "A Game of Thrones", 
                    ISBN = "9780553103540", 
                    Price = 24.99M, 
                    Stock = 40, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    Slug = SlugGenerator.Generate("A Game of Thrones")
                },
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000004"),
                    Title = "A Clash of Kings", 
                    ISBN = "9780553108033", 
                    Price = 24.99M, 
                    Stock = 35, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    Slug = SlugGenerator.Generate("A Clash of Kings")
                },
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000005"),
                    Title = "The Shining", 
                    ISBN = "9780385121675", 
                    Price = 17.99M, 
                    Stock = 30, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000005"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000009"),
                    Slug = SlugGenerator.Generate("The Shining")
                },
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000006"),
                    Title = "It", 
                    ISBN = "9780450411434", 
                    Price = 18.99M, 
                    Stock = 25, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000005"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000009"),
                    Slug = SlugGenerator.Generate("It")
                },
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000007"),
                    Title = "The Lord of the Rings: Fellowship", 
                    ISBN = "9780547928210", 
                    Price = 22.99M, 
                    Stock = 40, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000005"),
                    Slug = SlugGenerator.Generate("The Lord of the Rings: Fellowship")
                },
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000008"),
                    Title = "Murder on the Orient Express", 
                    ISBN = "9780062073501", 
                    Price = 14.99M, 
                    Stock = 30, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000004"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000006"),
                    Slug = SlugGenerator.Generate("Murder on the Orient Express")
                },
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000009"),
                    Title = "The Da Vinci Code", 
                    ISBN = "9780307474278", 
                    Price = 16.99M, 
                    Stock = 25, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000006"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000007"),
                    Slug = SlugGenerator.Generate("The Da Vinci Code")
                },
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000010"),
                    Title = "The Hunger Games", 
                    ISBN = "9780439023481", 
                    Price = 18.99M, 
                    Stock = 35, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000007"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000014"),
                    Slug = SlugGenerator.Generate("The Hunger Games")
                },
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000011"),
                    Title = "Catching Fire", 
                    ISBN = "9780439023498", 
                    Price = 18.99M, 
                    Stock = 35, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000007"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000014"),
                    Slug = SlugGenerator.Generate("Catching Fire")
                },
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000012"),
                    Title = "Mockingjay", 
                    ISBN = "9780439023511", 
                    Price = 18.99M, 
                    Stock = 35, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000007"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000014"),
                    Slug = SlugGenerator.Generate("Mockingjay")
                },
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000013"),
                    Title = "Foundation", 
                    ISBN = "9780553293357", 
                    Price = 15.99M, 
                    Stock = 25, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000010"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000014"),
                    Slug = SlugGenerator.Generate("Foundation")
                },
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000014"),
                    Title = "I, Robot", 
                    ISBN = "9780553294385", 
                    Price = 15.99M, 
                    Stock = 25, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000010"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000014"),
                    Slug = SlugGenerator.Generate("I, Robot")
                },
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000015"),
                    Title = "The Old Man and The Sea", 
                    ISBN = "9780684801223", 
                    Price = 12.99M, 
                    Stock = 20, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000008"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    Slug = SlugGenerator.Generate("The Old Man and The Sea")
                },
                new Book 
                { 
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000016"),
                    Title = "Adventures of Huckleberry Finn", 
                    ISBN = "9780486280615", 
                    Price = 11.99M, 
                    Stock = 20, 
                    AuthorId = Guid.Parse("20000000-0000-0000-0000-000000000009"),
                    CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    Slug = SlugGenerator.Generate("Adventures of Huckleberry Finn")
                },
            };

            modelBuilder.Entity<Book>().HasData(books);
        }
    }
}
