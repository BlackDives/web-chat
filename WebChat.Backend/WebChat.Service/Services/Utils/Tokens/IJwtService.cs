using Microsoft.IdentityModel.JsonWebTokens;
using WebChat.Shared.Common;
using WebChat.Shared.Models.Auth;
using WebChat.Shared.Models.Users;

namespace Webchat.Service.Services.Utils.Tokens;

public interface IJwtService
{
    JsonWebToken GenerateAccessToken(User user);
    
    JsonWebToken GenerateProfileCompletionToken(GoogleProfileCompleteClaims googleClaims);
    
    Task<JsonWebToken> GenerateRefreshTokenAsync(User user);
    
    Task<JsonWebToken> RefreshAccessTokenAsync(User user);
    Task<bool> IsTokenExpiredAsync(string token);
    Result<AccessTokenClaims> GetAccessTokenClaims(string token);

    Result<RefreshTokenClaims> GetRefreshTokenClaims(string refreshToken);

}