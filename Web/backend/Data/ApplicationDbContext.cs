using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Findgroup_Backend.Data
{
    public partial class ApplicationDbContext(IConfiguration configuration, IWebHostEnvironment env) : IdentityDbContext<User>
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IWebHostEnvironment _env = env;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!_env.IsDevelopment())
            {
                optionsBuilder.UseMySql(_configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(_configuration.GetConnectionString("DefaultConnection")));
            }
            else
            {
                optionsBuilder.UseInMemoryDatabase(_configuration.GetConnectionString("DevelopmentDB"));
            }
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; } 
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
