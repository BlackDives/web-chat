using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using WebChat.Service.Services.Auth.Interfaces;
using WebChat.Shared.Common;
using WebChat.Shared.Models.Auth;
using WebChat.Shared.Models.Users;

namespace Webchat.Service.Services.Utils.Tokens;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly JsonWebTokenHandler _tokenHandler;

    public JwtService(IConfiguration configuration, JsonWebTokenHandler tokenHandler, IRefreshTokenService refreshTokenService)
    {
        _configuration = configuration;
        _tokenHandler = tokenHandler;
        _refreshTokenService = refreshTokenService;
    }
    
    public JsonWebToken GenerateAccessToken(User user)
    {
        double expiresMinutes = _configuration.GetSection("JwtConfiguration:Access:TokenValidityMins").Get<double>();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfiguration:Access:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new Dictionary<string, object>
        {
            {JwtRegisteredClaimNames.Typ, "at+jwt" },
            { JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() },
            { JwtRegisteredClaimNames.UniqueName, user.Username },
            { JwtRegisteredClaimNames.Email, user.Email },
            { "uap", user.Id },
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
        
        return new JsonWebToken(unsignedToken);
    }
    public JsonWebToken GenerateProfileCompletionToken(GoogleProfileCompleteClaims googleClaims)
    {
        double expiresMinutes = _configuration.GetSection("JwtConfiguration:CompleteProfile:TokenValidityMins").Get<double>();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfiguration:CompleteProfile:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new Dictionary<string, object>
        {
            { JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() },
            { JwtRegisteredClaimNames.UniqueName, googleClaims.Email },
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
        //var signedToken = _tokenHandler.CreateToken(unsignedToken, credentials);
        
        return new JsonWebToken(unsignedToken);
    }
    public async Task<JsonWebToken> GenerateRefreshTokenAsync(User user)
    {
        double expiresMinutes = _configuration.GetSection("JwtConfiguration:Refresh:TokenValidityMins").Get<double>();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfiguration:Refresh:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var jwtId = Guid.NewGuid();
        var expirationDate = DateTime.UtcNow.AddMinutes(expiresMinutes);
        
        var claims = new Dictionary<string, object>
        {
            {JwtRegisteredClaimNames.Typ, "rt+jwt" },
            { JwtRegisteredClaimNames.Jti, jwtId.ToString() },
            { JwtRegisteredClaimNames.UniqueName, user.Username },
            { JwtRegisteredClaimNames.Email, user.Email },
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _configuration["JwtConfiguration:Audience"],
            Issuer = _configuration["JwtConfiguration:Issuer"],
            Claims = claims,
            IssuedAt = DateTime.UtcNow,
            Expires = expirationDate,
            SigningCredentials = credentials
        };
        
        var unsignedToken = _tokenHandler.CreateToken(tokenDescriptor);
        var refreshToken = new RefreshToken
        {
            Id = jwtId,
            Token = unsignedToken,
            UserId = user.Id,
            IsActive = true,
            IsRevoked = false,
            ExpiresOn = expirationDate,
        };
        var storeRefreshToken = await _refreshTokenService.AddRefreshTokenAsync(refreshToken);
        
        return new JsonWebToken(unsignedToken);
    }
    public async Task<JsonWebToken> RefreshAccessTokenAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsTokenExpiredAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token) || !_tokenHandler.CanReadToken(token))
        {
            return Task.FromResult(true);
        }

        try
        {
            var parsedToken = _tokenHandler.ReadJsonWebToken(token);
            return Task.FromResult(parsedToken.ValidTo <= DateTime.UtcNow);
        }
        catch (ArgumentException)
        {
            return Task.FromResult(true);
        }
    }

    public Result<AccessTokenClaims> GetAccessTokenClaims(string accessToken)
    {
        var parsedToken = _tokenHandler.ReadJsonWebToken(accessToken);
        var userId = parsedToken.Claims.FirstOrDefault(c => c.Type.ToLower() == "uap")?.Value;
        var username = parsedToken.Claims.FirstOrDefault(c => c.Type.ToLower() == "unique_name")?.Value;
        var userEmail = parsedToken.Claims.FirstOrDefault(c => c.Type.ToLower() == "email")?.Value;

        var results = new AccessTokenClaims
        {
            UserId = Guid.Parse(userId),
            Username = username,
            Email = userEmail,
        };
        
        return Result<AccessTokenClaims>.Ok(results);
    }

    public Result<RefreshTokenClaims> GetRefreshTokenClaims(string refreshToken)
    {
        var parsedToken = _tokenHandler.ReadJsonWebToken(refreshToken);
        var username = parsedToken.Claims.FirstOrDefault(c => c.Type.ToLower() == "unique_name")?.Value;
        var userEmail = parsedToken.Claims.FirstOrDefault(c => c.Type.ToLower() == "email")?.Value;

        var results = new RefreshTokenClaims
        {
            Username = username,
            Email = userEmail,
        };
        
        return Result<RefreshTokenClaims>.Ok(results);
    }
}
