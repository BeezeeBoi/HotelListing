using Microsoft.Build.Framework;

namespace HotelListing.API.Models.Region;

public class IncludeRegionDO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}