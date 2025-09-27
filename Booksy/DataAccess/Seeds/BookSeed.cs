using Booksy.Models.Entities.Books;

namespace Booksy.DataAccess.Seeds
{
    public static class BookSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Books.Any())
            {
                var hp = context.Authors.FirstOrDefault(a => a.Name == "J.K. Rowling");
                var fiction = context.Categories.FirstOrDefault(c => c.Name == "Fiction");

                if (hp != null && fiction != null)
                {
                    var books = new List<Book>
                    {
                        new Book
                        {
                            Title = "Harry Potter and the Philosopher's Stone",
                            Price = 19.99M,
                            Stock = 50,
                            ISBN = "9780747532699",
                            AuthorId = hp.Id,
                            CategoryId = fiction.Id
                        }
                    };

                    await context.Books.AddRangeAsync(books);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
