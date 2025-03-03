﻿using Findgroup_Backend.Models;
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
            // probably a switched off safety check for testing
            builder.HasOne(x => x.RefreshToken).WithOne(r => r.User).IsRequired(false);
            builder.HasMany(x => x.JoinedGroups)
                .WithMany(group => group.Users)
                .UsingEntity<Dictionary<string, object>>(
                "UserGroup",
                j => j.HasOne<Group>().WithMany().HasForeignKey("GroupId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId")
                );
            // Don't throw error if users don't have posts yet
            builder.HasMany(x => x.Posts).WithOne(r => r.User).IsRequired(false);
            var admin = new User
            {
                Id = "ADMIN",
                UserName = "admin"
                
            };
            builder.HasData(new User
            {
                Id = "Test",
                UserName = "Test1"
            },
            admin
            );
            var hash = hasher.HashPassword(admin, "admin");
            admin.PasswordHash = hash;
        }
    }
}