using Findgroup_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Findgroup_Backend.Configuration
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // probably a switched off safety check for testing
            builder.HasOne(x => x.RefreshToken).WithOne(r => r.User).IsRequired(false);
            // oh, btw did i mention to SEED DATA HERE??
        }
    }
}
