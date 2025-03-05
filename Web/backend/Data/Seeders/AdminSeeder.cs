using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Findgroup_Backend.Data.Seeders
{
    public class AdminSeeder
    {
        public static async Task SeedAdminAsync( UserManager<User> userManager)
        {
          User admin = new User() { UserName="admin"};
            await userManager.CreateAsync(admin, "admin");

        }
    }
}
