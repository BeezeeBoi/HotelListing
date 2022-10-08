using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Users;

public class UserLoginDO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "Your Password must be between {1} and {2} characters", MinimumLength = 7)]
    public string Password { get; set; }
}