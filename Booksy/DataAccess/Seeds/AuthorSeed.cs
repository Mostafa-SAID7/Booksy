using Booksy.Models.Entities.Books;

namespace Booksy.DataAccess.Seeds
{
    public static class AuthorSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Authors.Any())
            {
                var authors = new List<Author>
                {
                    new Author { Name = "J.K. Rowling", Bio = "Author of Harry Potter" },
                    new Author { Name = "George R.R. Martin", Bio = "Author of Game of Thrones" }
                };

                await context.Authors.AddRangeAsync(authors);
                await context.SaveChangesAsync();
            }
        }
    }
}
