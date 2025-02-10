using Microsoft.AspNetCore.Identity;

namespace Findgroup_Backend.Data.Seeders
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> manager)
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
        }
    }
}
