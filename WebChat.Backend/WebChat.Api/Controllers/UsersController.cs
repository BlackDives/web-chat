using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webchat.Service.Services.Utils.Tokens;

namespace WebChat.Api.Controllers;

[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IJwtService _jwtService;
    public UsersController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        /*
         * 1. Get access and refresh token
         * 2. check expiry
         * 3. if both existing and not expired -> return user details
         * 4. if both existing and access token expired -> reroute to refresh token controller
         * 5. if both existing and expired -> return a 401
         */
        
        if (!Request.Cookies.TryGetValue("AccessToken", out var accessToken))
        {
            return Unauthorized("No access token.");
        }
        
        var tokenIsExpired = await _jwtService.IsTokenExpiredAsync(accessToken);
        if (tokenIsExpired)
        {
            var yo = 2;
            return RedirectToAction(nameof(AuthenticationController.RefreshToken), "Authentication");
        }
        
        return Ok();
    }
}