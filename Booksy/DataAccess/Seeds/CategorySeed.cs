using Booksy.Models.Entities.Books;
using Booksy.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Booksy.DataAccess.Seeds
{
    /// <summary>
    /// Seeds default application categories
    /// All IDs are deterministic GUIDs for consistent FK relationships
    /// </summary>
    public static class CategorySeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var categories = new List<Category>
            {
                new Category 
                { 
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    Name = "Fiction",
                    Slug = SlugGenerator.Generate("Fiction")
                },
                new Category 
                { 
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                    Name = "Non-Fiction",
                    Slug = SlugGenerator.Generate("Non-Fiction")
                },
                new Category 
                { 
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                    Name = "Science",
                    Slug = SlugGenerator.Generate("Science")
                },
                new Category 
                { 
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000004"),
                    Name = "Children",
                    Slug = SlugGenerator.Generate("Children")
                },
                new Category 
                { 
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000005"),
                    Name = "Fantasy",
                    Slug = SlugGenerator.Generate("Fantasy")
                },
                new Category 
                { 
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000006"),
                    Name = "Mystery",
                    Slug = SlugGenerator.Generate("Mystery")
                },
                new Category 
                { 
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000007"),
                    Name = "Thriller",
                    Slug = SlugGenerator.Generate("Thriller")
                },
                new Category 
                { 
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000008"),
                    Name = "Romance",
                    Slug = SlugGenerator.Generate("Romance")
                },
                new Category 
                { 
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000009"),
                    Name = "Horror",
                    Slug = SlugGenerator.Generate("Horror")
                },
                new Category 
                { 
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000010"),
                    Name = "Biography",
                    Slug = SlugGenerator.Generate("Biography")
                },
                new Category 
                { 
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000011"),
                    Name = "Self-Help",
                    Slug = SlugGenerator.Generate("Self-Help")
                },
                new Category 
                { 
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000012"),
                    Name = "History",
                    Slug = SlugGenerator.Generate("History")
                },
                new Category 
                { 
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000013"),
                    Name = "Poetry",
                    Slug = SlugGenerator.Generate("Poetry")
                },
                new Category 
                { 
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000014"),
                    Name = "Science Fiction",
                    Slug = SlugGenerator.Generate("Science Fiction")
                }
            };

            modelBuilder.Entity<Category>().HasData(categories);
        }
    }
}
