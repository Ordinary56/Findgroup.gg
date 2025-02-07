using Findgroup_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Findgroup_Backend.Configuration
{
    public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category()
            {
                Id = 1,
                CategoryName = "League of Legends"
            },
            new Category()
            {
                Id = 2,
                CategoryName = "Valorant"
            },
            new Category()
            {
                Id = 3,
                CategoryName = "Apex Legends"
            },
            new Category()
            {
                Id = 4,
                CategoryName = "CS2"
            });
        }
    }
}
