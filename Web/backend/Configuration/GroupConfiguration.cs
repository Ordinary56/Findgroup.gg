﻿using Findgroup_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Findgroup_Backend.Configuration
{
    public sealed class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasMany(group => group.Users).WithMany(user => user.JoinedGroups)
                .UsingEntity<Dictionary<string, object>>("UserGroup",
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j => j.HasOne<Group>().WithMany().HasForeignKey("GroupId"));
            builder.Property(group => group.GroupName).IsRequired(true);
            builder.Property(group => group.MemberLimit).HasDefaultValue(1);
            Guid testId = new("416ef2a2-260c-420f-9838-f4a8904cfbe1");
            builder.HasData(new Group
            {
                Id = testId,
                GroupName = "League Team",
                MemberLimit = 5,
                Description = "A team for league",
                Users = []
            });
        }
    }
}
