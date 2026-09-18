using System.Security.Claims;
using WebChat.Service.Extensions;
using Webchat.Service.Services.Utils.Tokens;
using WebChat.Shared.Models.Auth;

namespace WebChat.Service.Services.Auth;

public class CompleteProfileService : ICompleteProfileService
{
    private readonly IJwtService _jwtService;
    
    public CompleteProfileService(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }
    
    public CompleteProfileUser GetCompleteProfileToken(ClaimsPrincipal userClaims)
    {
        var userEmail = userClaims.GetClaim(ClaimTypes.Email);
        var userName = userClaims.GetClaim(ClaimTypes.Name);
        
        var claims = new GoogleProfileCompleteClaims
        {
            Username = userName.Value,
            Email = userEmail.Value
        };
        
        var tempToken = _jwtService.GenerateProfileCompletionToken(claims);
        
        var results = new CompleteProfileUser
        {
            CompleteProfileToken = tempToken.EncodedToken
        };

        return results;
    }
}