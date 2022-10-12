using HotelListing.API.Core.Contracts;
using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    readonly IAuthManager _authManager;
    readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(IAuthManager authManager, ILogger<AuthenticationController> logger)
    {
        _authManager = authManager;
        _logger = logger;
    }

    // POST: api/Authentication/register
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Register([FromBody] UserDO userDO)
    {
        _logger.LogInformation($"Registration attempt for {userDO.Email}");

        try
        {
            var errors = await _authManager.Register(userDO);

            var identityErrors = errors.ToList();
            if (identityErrors.Any())
            {
                foreach (var error in identityErrors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error occurred in {nameof(Register)} - User Registration attempt for {userDO.Email}");
            return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
        }
    }

    // POST: api/Authentication/login
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Login([FromBody] UserLoginDO userLoginDO)
    {
        _logger.LogInformation($"Login attempt for {userLoginDO.Email}");

        try
        {
            var authResponse = await _authManager.Login(userLoginDO);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Something went wrong in the {nameof(Login)}");
            return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
        }

    }

    // POST: api/Authentication/refreshToken
    [HttpPost]
    [Route("refreshToken")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDO request)
    {
        try
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Something went wrong in {nameof(RefreshToken)}");
            return Problem($"Something went wrong in {nameof(RefreshToken)}", statusCode: 500);
        }
    }
}