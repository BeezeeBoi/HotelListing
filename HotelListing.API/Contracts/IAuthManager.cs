﻿using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts;

public interface IAuthManager
{
    Task<IEnumerable<IdentityError>> Register(UserDO userDO);

    Task<AuthResponseDO> Login(UserLoginDO userLoginDO);
}