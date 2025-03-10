using Findgroup_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Findgroup_Backend.Configuration
{
    public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasIndex(t => t.TokenHash).IsUnique(true);
            builder.Property(t => t.TokenHash).HasMaxLength(200);
        }
    }
}
