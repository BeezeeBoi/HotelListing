﻿using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Users;

public class UserDO : UserLoginDO
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
}