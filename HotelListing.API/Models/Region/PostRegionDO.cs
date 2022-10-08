using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Region;

public class PostRegionDO : BaseRegionDO
{
    [Required]
    public string Name { get; set; }
}