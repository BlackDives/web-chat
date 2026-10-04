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
        if (!Request.Cookies.TryGetValue("AccessToken", out var accessToken))
        {
            return Unauthorized("No access token.");
        }
        
        var tokenIsExpired = await _jwtService.IsTokenExpiredAsync(accessToken);
        if (tokenIsExpired)
        {
            return RedirectToAction(nameof(AuthenticationController.RefreshToken), "Authentication");
        }
        
        var accessTokenClaimsResult = _jwtService.GetAccessTokenClaims(accessToken);
        var accessTokenClaims = accessTokenClaimsResult.Value;
        
        return Ok(accessTokenClaims);
    }
}