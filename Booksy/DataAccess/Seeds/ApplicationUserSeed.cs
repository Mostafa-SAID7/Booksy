using Booksy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Booksy.DataAccess.Seeds
{
    /// <summary>
    /// Seeds default application users for testing
    /// Includes: 1 Admin, 2 Customers
    /// IDs are deterministic for consistent FK relationships
    /// </summary>
    public static class ApplicationUserSeed
    {
        // Deterministic IDs for FK relationships
        private const string AdminId = "00000000-0000-0000-0000-000000000001";
        private const string Customer1Id = "00000000-0000-0000-0000-000000000002";
        private const string Customer2Id = "00000000-0000-0000-0000-000000000003";

        public static void Seed(ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            var users = new List<ApplicationUser>
            {
                // Admin user
                new ApplicationUser
                {
                    Id = AdminId,
                    UserName = "admin@booksy.com",
                    NormalizedUserName = "ADMIN@BOOKSY.COM",
                    Email = "admin@booksy.com",
                    NormalizedEmail = "ADMIN@BOOKSY.COM",
                    EmailConfirmed = true,
                    Name = "System Admin",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword(null!, "Admin@123"),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                // Customer 1
                new ApplicationUser
                {
                    Id = Customer1Id,
                    UserName = "customer1@booksy.com",
                    NormalizedUserName = "CUSTOMER1@BOOKSY.COM",
                    Email = "customer1@booksy.com",
                    NormalizedEmail = "CUSTOMER1@BOOKSY.COM",
                    EmailConfirmed = true,
                    Name = "Alice",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword(null!, "Customer@123"),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                // Customer 2
                new ApplicationUser
                {
                    Id = Customer2Id,
                    UserName = "customer2@booksy.com",
                    NormalizedUserName = "CUSTOMER2@BOOKSY.COM",
                    Email = "customer2@booksy.com",
                    NormalizedEmail = "CUSTOMER2@BOOKSY.COM",
                    EmailConfirmed = true,
                    Name = "Bob",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword(null!, "Customer@123"),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            };

            modelBuilder.Entity<ApplicationUser>().HasData(users);
        }
    }
}
