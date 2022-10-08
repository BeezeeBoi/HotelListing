using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Repository;

public class AuthManager : IAuthManager
{
    readonly IMapper _mapper;
    readonly UserManager<User> _userManager;

    public AuthManager(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<bool> Login(UserLoginDO loginDO)
    {
        var isUserValid = false;
        
        try
        {
            var user = await _userManager.FindByEmailAsync(loginDO.Email);
            if (user == null)
            {
                return default;
            }
            
            isUserValid = await _userManager.CheckPasswordAsync(user, loginDO.Password);
            if (!isUserValid)
            {
                return default;
            }
        }
        catch (Exception)
        {
            // ignored
        }
        
        return isUserValid;
    }

    public async Task<IEnumerable<IdentityError>> Register(UserDO userDO)
    {
        var user = _mapper.Map<User>(userDO);
        user.UserName = userDO.Email;

        var result = await _userManager.CreateAsync(user, userDO.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
        }

        return result.Errors;
    }
}