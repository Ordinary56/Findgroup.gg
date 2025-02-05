
using Findgroup_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Findgroup_Backend.Configuration
{
    public sealed class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasOne(t => t.Post).WithMany(p => p.Tags).HasForeignKey(t => t.PostId).IsRequired(false);
            // TODO: Seed Data here

        }
    }
}
