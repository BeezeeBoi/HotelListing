using System.ComponentModel.DataAnnotations;
using HotelListing.API.Core.Models.Region;

namespace HotelListing.API.Core.Models.Country;

public class PostCountryDO : BaseCountryDO
{
    [Required]
    public GetRegionDO Region { get; set; }
}