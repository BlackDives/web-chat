using System.Security.Claims;
using WebChat.Service.Extensions;
using WebChat.Service.Services.Users;
using Webchat.Service.Services.Utils.Tokens;
using WebChat.Shared.Common;
using WebChat.Shared.Enums;
using WebChat.Shared.Models.Auth;

namespace WebChat.Service.Services.Auth;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public GoogleAuthService(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }
    
    public async Task<Result<AuthenticatedUser>> GoogleSignInAsync(ClaimsPrincipal userClaims)
    {
        var userEmail = userClaims.GetClaim(ClaimTypes.Email);
        var userName = userClaims.GetClaim(ClaimTypes.Name);
        var userResult = await _userService.FindUserByEmailAsync(userEmail.Value);
        
        if (!userResult.Success)
        {
            return Result<AuthenticatedUser>.Fail(ResultMessage.UserNotFound, ServiceErrorEnum.NotFound);
        }
        
        var user = userResult.Value;
        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = await _jwtService.GenerateRefreshTokenAsync(user);
        
        var results = new AuthenticatedUser
        {
            AccessToken = accessToken.EncodedToken,
            RefreshToken = refreshToken.EncodedToken,
            Email = userEmail.Value,
            Username = userName.Value,
        };
            
        return Result<AuthenticatedUser>.Ok(results);
    }
}