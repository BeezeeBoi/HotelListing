using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Core.Models.Region;

public class BaseRegionDO
{
    [Required]
    public int Id { get; set; }
}