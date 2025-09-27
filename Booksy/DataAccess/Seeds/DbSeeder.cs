using Booksy.DataAccess;
using Booksy.Utility.DBInitializer;
using Booksy.Utility.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Booksy.DataAccess.Seeds
{
    public static class DbSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            ApplicationUserSeed.Seed(modelBuilder);
            CategorySeed.Seed(modelBuilder);
            AuthorSeed.Seed(modelBuilder);
            BookSeed.Seed(modelBuilder);
            ReviewSeed.Seed(modelBuilder);
        }
    }
}
