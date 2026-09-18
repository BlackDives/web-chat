using Microsoft.IdentityModel.JsonWebTokens;
using WebChat.Shared.Models.Auth;
using WebChat.Shared.Models.Users;

namespace Webchat.Service.Services.Utils.Tokens;

public interface IJwtService
{
    JsonWebToken GenerateAccessToken(User user);
    
    JsonWebToken GenerateProfileCompletionToken(GoogleProfileCompleteClaims googleClaims);
    
    Task<JsonWebToken> GenerateRefreshTokenAsync(User user);
    
    Task<JsonWebToken> RefreshAccessTokenAsync(User user);
    
}