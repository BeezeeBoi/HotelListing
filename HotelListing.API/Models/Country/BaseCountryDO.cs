using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Country
{
    public abstract class BaseCountryDO
    {
        [Required]
        public int Name { get; set; }
        public int ShortName { get; set; }
    }
}
