using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebChat.Service.Extensions;
using WebChat.Service.Services.Users;
using Webchat.Service.Services.Utils.Tokens;
using WebChat.Shared.Common;
using WebChat.Shared.Enums;
using WebChat.Shared.Models.Auth;
using WebChat.Shared.Models.Users;

namespace WebChat.Service.Services.Auth;

public class CompleteProfileService : ICompleteProfileService
{
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    
    public CompleteProfileService(IJwtService jwtService, IUserService userService, JwtSecurityTokenHandler tokenHandler)
    {
        _jwtService = jwtService;
        _userService = userService;
        _jwtSecurityTokenHandler = tokenHandler;
    }


    public async Task<Result<AuthenticatedUser?>> CompleteUserCreationAsync(CompletedUserProfile completedUserProfile, string completeProfileToken)
    {
        var parsedToken = _jwtSecurityTokenHandler.ReadJwtToken(completeProfileToken);
        var userEmail = parsedToken.Claims.FirstOrDefault(c => c.Type.ToLower() == "unique_name")?.Value;
        var userResult = await _userService.FindUserByUsernameAsync(completedUserProfile.Username);
        if (userResult.Success)
        {
            return Result<AuthenticatedUser?>.Fail(ResultMessage.UsernameTaken, ServiceErrorEnum.Conflict);
        }

        var createdUserResult = await _userService.CreateUserAsync(completedUserProfile, userEmail);

        if (!createdUserResult.Success)
        {
            return Result<AuthenticatedUser?>.Fail(createdUserResult.ErrorMessage, createdUserResult.ErrorType);
        }
        
        var user = createdUserResult.Value;
        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = await _jwtService.GenerateRefreshTokenAsync(user);

        var result = new AuthenticatedUser
        {
            AccessToken = accessToken.EncodedToken,
            RefreshToken = refreshToken.EncodedToken,
            Username = user.Username,
            Email = user.Email,
        };
        
        return Result<AuthenticatedUser?>.Ok(result);
    }
}