using Findgroup_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Findgroup_Backend.Configuration
{
    public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Token).HasMaxLength(200);
            builder.HasIndex(x => x.Token).IsUnique();
            builder.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId);
        }
    }
}
