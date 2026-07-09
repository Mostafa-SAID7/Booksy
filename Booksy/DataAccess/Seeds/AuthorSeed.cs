using Booksy.Models.Entities.Books;
using Booksy.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Booksy.DataAccess.Seeds
{
    /// <summary>
    /// Seeds default application authors
    /// All IDs are deterministic GUIDs for consistent FK relationships
    /// </summary>
    public static class AuthorSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var authors = new List<Author>
            {
                new Author 
                { 
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    Name = "J.K. Rowling", 
                    Bio = "Author of Harry Potter series",
                    Slug = SlugGenerator.Generate("J.K. Rowling")
                },
                new Author 
                { 
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    Name = "George R.R. Martin", 
                    Bio = "Author of Game of Thrones series",
                    Slug = SlugGenerator.Generate("George R.R. Martin")
                },
                new Author 
                { 
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                    Name = "J.R.R. Tolkien", 
                    Bio = "Author of The Lord of the Rings",
                    Slug = SlugGenerator.Generate("J.R.R. Tolkien")
                },
                new Author 
                { 
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000004"),
                    Name = "Agatha Christie", 
                    Bio = "Famous mystery and crime writer",
                    Slug = SlugGenerator.Generate("Agatha Christie")
                },
                new Author 
                { 
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000005"),
                    Name = "Stephen King", 
                    Bio = "Renowned horror and thriller author",
                    Slug = SlugGenerator.Generate("Stephen King")
                },
                new Author 
                { 
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000006"),
                    Name = "Dan Brown", 
                    Bio = "Author of The Da Vinci Code and Robert Langdon series",
                    Slug = SlugGenerator.Generate("Dan Brown")
                },
                new Author 
                { 
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000007"),
                    Name = "Suzanne Collins", 
                    Bio = "Author of The Hunger Games trilogy",
                    Slug = SlugGenerator.Generate("Suzanne Collins")
                },
                new Author 
                { 
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000008"),
                    Name = "Ernest Hemingway", 
                    Bio = "American novelist and Nobel Prize winner",
                    Slug = SlugGenerator.Generate("Ernest Hemingway")
                },
                new Author 
                { 
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000009"),
                    Name = "Mark Twain", 
                    Bio = "American writer and humorist",
                    Slug = SlugGenerator.Generate("Mark Twain")
                },
                new Author 
                { 
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000010"),
                    Name = "Isaac Asimov", 
                    Bio = "Science fiction and non-fiction author",
                    Slug = SlugGenerator.Generate("Isaac Asimov")
                }
            };

            modelBuilder.Entity<Author>().HasData(authors);
        }
    }
}
