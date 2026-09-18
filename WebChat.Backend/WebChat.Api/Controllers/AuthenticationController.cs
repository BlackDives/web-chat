using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebChat.Api.Policies;
using WebChat.Service.Services.Auth;
using WebChat.Api.Extensions.Mappers;

namespace WebChat.Api.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IGoogleAuthService _googleAuthService;
    private readonly ICompleteProfileService _completeProfileService;
    
    public AuthenticationController(IGoogleAuthService googleAuthService, ICompleteProfileService completeProfileService)
    {
        _googleAuthService = googleAuthService;
        _completeProfileService = completeProfileService;
    }
    
    [HttpPost("google/login")]
    [Authorize(AuthenticationSchemes = $"{AuthenticationPolicies.GoogleAuthScheme}")]
    public async Task<IActionResult> GoogleSignIn()
    {
        var getResults = await _googleAuthService.GoogleSignInAsync(User);
        if (!getResults.Success)
        {
            var tempToken = _completeProfileService.GetCompleteProfileToken(User);
            return Ok(tempToken);
        }

        var results = getResults.Value.ToDto();
        
        return Ok(results);
    }
    
}