using HotelListing.API.Models.Hotel;
using HotelListing.API.Models.Region;

namespace HotelListing.API.Models.Country;

public class CountryDO : BaseCountryDO
{
    public int Id { get; set; }

    public RegionDO Region { get; set; }

    public List<HotelDO> HotelList { get; set; }
}