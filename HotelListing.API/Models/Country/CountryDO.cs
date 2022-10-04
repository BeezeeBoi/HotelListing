using HotelListing.API.Models.Hotel;

namespace HotelListing.API.Models.Country;

public class CountryDO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Region { get; set; }

    public List<HotelDO> Hotels { get; set; }
}