using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Findgroup_Backend.Configuration
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            PasswordHasher<User> hasher = new();

            builder.HasOne(x => x.RefreshToken).WithOne(r => r.User).HasForeignKey<RefreshToken>(token => token.UserId).IsRequired(false);
            builder.HasMany(x => x.JoinedGroups)
                .WithMany(group => group.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserGroup",
                    j => j.HasOne<Group>().WithMany().HasForeignKey("GroupId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId")
                );


            builder.HasMany(x => x.Posts).WithOne(r => r.Creator).IsRequired(false);
            builder.HasMany(x => x.Posts).WithOne(r => r.Creator).IsRequired(false);
            var testUser = new User
            {
                Id = "Test",
                UserName = "Test1",
                NormalizedUserName = "TEST1",  // Ezt se felejtsd el!
                PasswordHash = hasher.HashPassword(null, "test123")
            };
            var admin = new User
            {
                Id = "ADMIN",
                UserName = "admin",
                NormalizedUserName = "ADMIN",  // Fontos: Identity elvárja a normalized nevet!
                PasswordHash = hasher.HashPassword(null, "admin")

            };
            builder.HasData(testUser, admin);
        }
    }
}
