using HotelListing.API.Models.Region;

namespace HotelListing.API.Models.Country;

public class GetCountryDO : BaseCountryDO
{
    public int Id { get; set; }

    public IncludeRegionDO Region { get; set; }
}