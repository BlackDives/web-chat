using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using web_api.Data;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace web_api.Services;

public class JWTService : IJWTService
{
    private readonly ApplicationDbContext _db;
    private readonly IConfiguration _configuration;
    
    public JWTService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _db = dbContext;
        _configuration = configuration;
    }

    public string GenerateToken(ApplicationUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTConfiguration:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserId", user.Id.ToString()),
        };

        var token = new JwtSecurityToken(_configuration["JWTConfiguration:Issuer"],
            _configuration["JWTConfiguration:Audience"],
            claims, 
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}