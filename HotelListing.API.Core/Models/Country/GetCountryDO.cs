using HotelListing.API.Core.Models.Region;

namespace HotelListing.API.Core.Models.Country;

public class GetCountryDO : BaseCountryDO
{
    public int Id { get; set; }

    public IncludeRegionDO Region { get; set; }
}