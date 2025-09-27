using Microsoft.EntityFrameworkCore;

namespace Booksy.DataAccess.Seeds
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Apply migrations before seeding
            await context.Database.MigrateAsync();

            // Call individual seeders
            await IdentitySeed.SeedAsync(context, serviceProvider);
            await CategorySeed.SeedAsync(context);
            await AuthorSeed.SeedAsync(context);
            await BookSeed.SeedAsync(context);
        }
    }
}
