using Booksy.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Booksy.DataAccess.Seeds
{
    /// <summary>
    /// Orchestrates all database seeding operations
    /// Called during OnModelCreating to populate initial data
    /// 
    /// Seed Order (respects FK dependencies):
    /// 1. ApplicationUsers - Base user data, no FK dependencies
    /// 2. Categories - No FK dependencies
    /// 3. Authors - No FK dependencies
    /// 4. Books - FK: CategoryId, AuthorId (requires Categories + Authors first)
    /// 5. Reviews - FK: BookId, UserId (requires Books + Users first)
    /// </summary>
    public static class DbSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Step 1: Seed independent entities first
            ApplicationUserSeed.Seed(modelBuilder);
            CategorySeed.Seed(modelBuilder);
            AuthorSeed.Seed(modelBuilder);

            // Step 2: Seed entities with FK dependencies
            BookSeed.Seed(modelBuilder);
            ReviewSeed.Seed(modelBuilder);
        }
    }
}
