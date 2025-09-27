using Booksy.Models.Entities.Books;

namespace Booksy.DataAccess.Seeds
{
    public static class CategorySeed
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Fiction" },
                    new Category { Name = "Non-Fiction" },
                    new Category { Name = "Science" },
                    new Category { Name = "Children" }
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }
        }
    }
}
