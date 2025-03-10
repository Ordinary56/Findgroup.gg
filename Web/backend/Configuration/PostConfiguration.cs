﻿using Findgroup_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Findgroup_Backend.Configuration
{
    public sealed class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasOne(post => post.Creator).WithMany(user => user.Posts).HasForeignKey(post => post.UserId).IsRequired(false);
            builder.HasOne(post => post.Category).WithMany(c => c.Posts).HasForeignKey(post => post.CategoryId).IsRequired(false);
            builder.Property(p => p.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.HasOne(p => p.Group).WithOne(g => g.Post).HasForeignKey<Group>(g => g.PostId).IsRequired(false);
            builder.HasData(new Post
            {
                Id = -1,
                IsActive = true,
                Title = "Leage Casual",
                Content = "Hiii :3. needs some friends in league. I don't have friends irl :c (ranked solo/duo)",
                CategoryId = 1,
                CreatedDate = new DateTime(2025, 01, 02),
                UserId = "Test",
            },
            new Post
            {
                Id = -2,
                IsActive = true,
                Title = "Apex casual",
                Content = "Needs a team in Apex pls.",
                CategoryId = 3,
                CreatedDate = new DateTime(2025, 01, 02),
                UserId = "Test"
            },
            new Post
            {
                Id = -3,
                IsActive = true,
                Title = "DMG Ranked CS2",
                Content = "I want 2 people in DMG matchmaking (CS2).",
                CategoryId = 4,
                CreatedDate = new DateTime(2025, 01, 02),
                UserId = "Test",
            });
        }
    }
}
