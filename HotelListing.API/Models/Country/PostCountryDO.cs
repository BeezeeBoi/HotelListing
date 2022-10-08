using System.ComponentModel.DataAnnotations;
using HotelListing.API.Models.Region;

namespace HotelListing.API.Models.Country;

public class PostCountryDO : BaseCountryDO
{
    [Required]
    public GetRegionDO Region { get; set; }
}