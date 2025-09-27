using Booksy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Booksy.DataAccess.Seeds
{
    public static class ApplicationUserSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // IDs must be deterministic for FK relationships
            var adminId = "00000000-0000-0000-0000-000000000001";
            var customer1Id = "00000000-0000-0000-0000-000000000002";
            var customer2Id = "00000000-0000-0000-0000-000000000003";

            var hasher = new PasswordHasher<ApplicationUser>();

            var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = adminId,
                    UserName = "admin@booksy.com",
                    NormalizedUserName = "ADMIN@BOOKSY.COM",
                    Email = "admin@booksy.com",
                    NormalizedEmail = "ADMIN@BOOKSY.COM",
                    EmailConfirmed = true,
                    Name = "System Admin",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword(null!, "Admin@123")
                },
                new ApplicationUser
                {
                    Id = customer1Id,
                    UserName = "customer1@booksy.com",
                    NormalizedUserName = "CUSTOMER1@BOOKSY.COM",
                    Email = "customer1@booksy.com",
                    NormalizedEmail = "CUSTOMER1@BOOKSY.COM",
                    EmailConfirmed = true,
                    Name = "Alice",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword(null!, "Customer@123")
                },
                new ApplicationUser
                {
                    Id = customer2Id,
                    UserName = "customer2@booksy.com",
                    NormalizedUserName = "CUSTOMER2@BOOKSY.COM",
                    Email = "customer2@booksy.com",
                    NormalizedEmail = "CUSTOMER2@BOOKSY.COM",
                    EmailConfirmed = true,
                    Name = "Bob",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword(null!, "Customer@123")
                }
            };

            modelBuilder.Entity<ApplicationUser>().HasData(users);
        }
    }
}
