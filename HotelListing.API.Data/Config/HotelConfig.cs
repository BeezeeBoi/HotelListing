using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Config;

public class HotelConfig : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasData(
            new Hotel
            {
                Id = 1,
                Name = "Holiday Inn",
                Address = "George Town",
                CountryId = 1,
                Rating = 3.5
            },
            new Hotel
            {
                Id = 2,
                Name = "Hilton",
                Address = "Knoxville",
                CountryId = 2,
                Rating = 4.5
            });
    }
}