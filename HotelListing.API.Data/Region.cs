using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.Data;

public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }

    [ForeignKey(nameof(CountryId))]
    public int CountryId { get; set; }
    public Country Country { get; set; }
}