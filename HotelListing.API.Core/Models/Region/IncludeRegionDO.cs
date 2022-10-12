using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Core.Models.Region;

public class IncludeRegionDO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}