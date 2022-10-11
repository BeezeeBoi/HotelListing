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
    readonly ILogger<AuthManager> _logger;
    User _user;

    const string LoginProvider = "HotelListAPI";
    const string RefreshToken = "RefreshToken";

    public AuthManager(IMapper mapper, UserManager<User> userManager, IConfiguration configuration, ILogger<AuthManager> logger)
    {
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;

        _userManager.RegisterTokenProvider(LoginProvider, new EmailTokenProvider<User>());
    }

    public async Task<AuthResponseDO> Login(UserLoginDO loginDO)
    {
        _logger.LogInformation($"User is logging in {loginDO.Email}");

        try
        {
            _user = await _userManager.FindByEmailAsync(loginDO.Email);
            bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDO.Password);

            if (_user == null || isValidUser == false)
            {
                return null;
            }

            var token = await GenerateToken();

            return new AuthResponseDO
            {
                Token = token,
                UserId = _user.Id,
                RefreshToken = await CreateRefreshToken()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error has occurred in {nameof(Login)}, with the user {loginDO.Email}");
            return new AuthResponseDO();
        }
    }

    public async Task<string> CreateRefreshToken()
    {
        try
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, LoginProvider, RefreshToken);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, LoginProvider, RefreshToken);
            var result =
                await _userManager.SetAuthenticationTokenAsync(_user, LoginProvider, RefreshToken, newRefreshToken);

            var token = await _userManager.GetAuthenticationTokenAsync(_user, LoginProvider, RefreshToken);

            return newRefreshToken;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error has occurred in {nameof(CreateRefreshToken)}");
            return null;
        }
    }

    public async Task<AuthResponseDO> VerifyRefreshToken(AuthResponseDO request)
    {
        try
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;

            _user = await _userManager.FindByEmailAsync(username);

            if (_user == null || _user.Id != request.UserId)
            {
                return null;
            }

            var isValidRefreshToken =
                await _userManager.VerifyUserTokenAsync(_user, LoginProvider, RefreshToken, request.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await GenerateToken();
                return new AuthResponseDO
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error occurred in {nameof(VerifyRefreshToken)}");
            return null;
        }
    }

    public async Task<IEnumerable<IdentityError>> Register(UserDO userDO)
    {
        _logger.LogInformation($"Registration attempt for {userDO.Email}");

        try
        {
            _user = _mapper.Map<User>(userDO);
            _user.UserName = userDO.Email;

            var result = await _userManager.CreateAsync(_user, userDO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "User");
            }

            return result.Errors;
        }
        catch (Exception e)
        {
            _logger.LogError(e,$"Registration attempt failed for {userDO.Email}");
            return null;
        }
    }

    async Task<string> GenerateToken()
    {
        try
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Sub, _user.Email),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Email, _user.Email),
                }
                .Union(userClaims)
                .Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception e)
        {
            _logger.LogError(e,$"Error occurred in {nameof(GenerateToken)}");
            return null;
        }
    }
}