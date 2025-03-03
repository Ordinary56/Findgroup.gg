using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Findgroup_Backend.Data.Seeders
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> manager, UserManager<User> userManager)
        {
            string[] rolesNames = ["User", "Admin"];
            foreach (var item in rolesNames)
            {
                var roleExists = await manager.RoleExistsAsync(item);
                if(!roleExists)
                {
                    await manager.CreateAsync(new IdentityRole(item));
                }
            }

            User? admin = await userManager.FindByNameAsync("admin");
            if (admin != null) 
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }

        }
    }
}
