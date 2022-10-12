using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Config;

public class RegionConfig : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.HasData(
            new Region
            {
                Id = 1,
                Name = "Central America",
                CountryId = 1
            },
            new Region
            {
                Id = 2,
                Name = "North America",
                CountryId = 2
            });
    }
}