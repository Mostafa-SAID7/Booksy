using Booksy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System.Web.Providers.Entities;

namespace Booksy.DataAccess.Seeds
{
    public static class IdentitySeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Admin", "Customer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Seed admin user
            var adminEmail = "admin@booksy.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Name = "System Admin",
                    City = "New York",
                    State = "NY",
                    ZipCode = "10001"
                };

                var result = await userManager.CreateAsync(user, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
