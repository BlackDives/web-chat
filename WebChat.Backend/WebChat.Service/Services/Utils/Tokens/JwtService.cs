using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using WebChat.Shared.Models.Auth;
using WebChat.Shared.Models.Users;

namespace Webchat.Service.Services.Utils.Tokens;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly JsonWebTokenHandler _tokenHandler;

    public JwtService(IConfiguration configuration, JsonWebTokenHandler tokenHandler)
    {
        _configuration = configuration;
        _tokenHandler = tokenHandler;
    }
    
    public JsonWebToken GenerateAccessToken(User user)
    {
        double expiresMinutes = _configuration.GetSection("JwtConfiguration:AccessToken:TokenValidityMins").Get<double>();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfiguration:AccessToken:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new Dictionary<string, object>
        {
            { JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() },
            { JwtRegisteredClaimNames.UniqueName, user.Username },
            { JwtRegisteredClaimNames.Email, user.Email },
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _configuration["JwtConfiguration:Audience"],
            Issuer = _configuration["JwtConfiguration:Issuer"],
            Claims = claims,
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(expiresMinutes),
            SigningCredentials = credentials
        };
        
        var unsignedToken = _tokenHandler.CreateToken(tokenDescriptor);
        var signedToken = _tokenHandler.CreateToken(unsignedToken, credentials);
        
        return new JsonWebToken(signedToken);
    }

    public JsonWebToken GenerateProfileCompletionToken(GoogleProfileCompleteClaims googleClaims)
    {
        double expiresMinutes = _configuration.GetSection("JwtConfiguration:CompleteProfile:TokenValidityMins").Get<double>();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfiguration:CompleteProfile:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new Dictionary<string, object>
        {
            { JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() },
            { JwtRegisteredClaimNames.UniqueName, googleClaims.Username },
            { JwtRegisteredClaimNames.Email, googleClaims.Email },
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _configuration["JwtConfiguration:Audience"],
            Issuer = _configuration["JwtConfiguration:Issuer"],
            Claims = claims,
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(expiresMinutes),
            SigningCredentials = credentials
        };
        
        var unsignedToken = _tokenHandler.CreateToken(tokenDescriptor);
        var signedToken = _tokenHandler.CreateToken(unsignedToken, credentials);
        
        return new JsonWebToken(signedToken);
    }
    public async Task<JsonWebToken> GenerateRefreshTokenAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<JsonWebToken> RefreshAccessTokenAsync(User user)
    {
        throw new NotImplementedException();
    }
}