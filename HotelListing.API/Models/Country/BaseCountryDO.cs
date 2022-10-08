using System.ComponentModel.DataAnnotations;
using HotelListing.API.Models.Region;

namespace HotelListing.API.Models.Country;

public abstract class BaseCountryDO
{
    [Required]
    public string Name { get; set; }
    public string ShortName { get; set; }

    public IncludeRegionDO Region { get; set; }
}