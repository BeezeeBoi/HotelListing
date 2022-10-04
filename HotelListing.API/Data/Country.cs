namespace HotelListing.API.Data;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Region { get; set; }

    public virtual IList<Hotel> HotelList { get; set; }
}