using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Region;

public class BaseRegionDO
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}