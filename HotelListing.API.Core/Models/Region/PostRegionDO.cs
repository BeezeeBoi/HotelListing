using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Core.Models.Region;

public class PostRegionDO : BaseRegionDO
{
    [Required]
    public string Name { get; set; }
}