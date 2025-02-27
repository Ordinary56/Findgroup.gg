using Activity = Findgroup_Backend.Models.Activity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Findgroup_Backend.Helpers;

namespace Findgroup_Backend.Configuration
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.HasData(CsvDataLoader.LoadActivity());
        }
    }
}
