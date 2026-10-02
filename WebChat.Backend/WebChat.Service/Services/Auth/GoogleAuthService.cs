using System.Security.Claims;
using WebChat.Service.Extensions;
using WebChat.Service.Services.Users;
using Webchat.Service.Services.Utils.Tokens;
using WebChat.Shared.Common;
using WebChat.Shared.Enums;
using WebChat.Shared.Models.Auth;
using System.IdentityModel.Tokens.Jwt;

namespace WebChat.Service.Services.Auth;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public GoogleAuthService(IUserService userService, IJwtService jwtService, JwtSecurityTokenHandler tokenHandler)
    {
        _userService = userService;
        _jwtService = jwtService;
        _jwtSecurityTokenHandler = tokenHandler;
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

    /// <inheritdoc />
    public async Task<Result<AuthenticatedUser?>> GoogleSignInAsync(string googleIdToken)
    {
        var parsedToken = _jwtSecurityTokenHandler.ReadJwtToken(googleIdToken);
        var userEmail = parsedToken.Claims.FirstOrDefault(c => c.Type == "email");
        if (userEmail == null)
        {
            return Result<AuthenticatedUser?>.Fail(ResultMessage.EmailNotFoundInToken, ServiceErrorEnum.NotFound);
        }
        
        var userResult = await _userService.FindUserByEmailAsync(userEmail.Value);
        
        if (!userResult.Success)
        {
            return Result<AuthenticatedUser?>.Fail(ResultMessage.UserNotFound, ServiceErrorEnum.NotFound);
        }
        
        var user = userResult.Value;
        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = await _jwtService.GenerateRefreshTokenAsync(user);
        
        var results = new AuthenticatedUser
        {
            AccessToken = accessToken.EncodedToken,
            RefreshToken = refreshToken.EncodedToken,
            Email = user.Email,
            Username = user.Username,
        };
            
        return Result<AuthenticatedUser?>.Ok(results);
    }

    /// <inheritdoc />
    public async Task<Result<CompleteProfileUser>> GoogleSignUpAsync(string googleIdToken)
    {
        var parsedToken = _jwtSecurityTokenHandler.ReadJwtToken(googleIdToken);
        var userEmail = parsedToken.Claims.FirstOrDefault(c => c.Type == "email");

        var completeProfileToken = _jwtService.GenerateProfileCompletionToken(new GoogleProfileCompleteClaims
        {
            Email = userEmail.Value,
        });

        var result = new CompleteProfileUser
        {
            CompleteProfileToken = completeProfileToken.EncodedToken,
        };

        return Result<CompleteProfileUser>.Ok(result);
    }
}