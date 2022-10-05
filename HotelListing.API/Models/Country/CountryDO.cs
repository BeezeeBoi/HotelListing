using HotelListing.API.Models.Hotel;

namespace HotelListing.API.Models.Country;

public class CountryDO : BaseCountryDO
{
    public int Id { get; set; }

    public List<HotelDO> HotelList { get; set; }
}