using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Hotel;

public class BaseHotelDO
{
    [Required]
    public int Id { get; set; }
}