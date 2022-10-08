using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace HotelListing.API.Repository;

public class AuthManager : IAuthManager
{
    readonly IMapper _mapper;
    readonly UserManager<User> _userManager;
    readonly IConfiguration _configuration;

    public AuthManager(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<AuthResponseDO> Login(UserLoginDO loginDO)
    {
        var user = await _userManager.FindByEmailAsync(loginDO.Email);
        bool isValidUser = await _userManager.CheckPasswordAsync(user, loginDO.Password);

        if (user == null || isValidUser == false)
        {
            return null;
        }

        var token = await GenerateToken(user);

        return new AuthResponseDO
        {
            Token = token,
            UserId = user.Id
        };
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

    async Task<string> GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
        var userClaims = await _userManager.GetClaimsAsync(user);
        
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        }
            .Union(userClaims)
            .Union(roleClaims);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
            signingCredentials:credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}