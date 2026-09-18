using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebChat.Api.Policies;

namespace WebChat.Api.Extensions.ServiceExtensions;

internal static class AuthServiceCollectionExtensions
{
    public static IServiceCollection AddAppAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication()
            .AddJwtBearer(AuthenticationPolicies.BearerScheme, options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JWTConfiguration:Issuer"],
                    ValidAudience = configuration["JWTConfiguration:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfiguration:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                };
            })
            .AddJwtBearer(AuthenticationPolicies.RefreshTokenScheme, options =>
            {
                options.RequireHttpsMetadata = false;
            })
            .AddJwtBearer(AuthenticationPolicies.CompleteProfileScheme, options =>
            {
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JWTConfiguration:Issuer"],
                    ValidAudience = configuration["JWTConfiguration:Audience"],
                    IssuerSigningKey = new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfiguration:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                };
            })
            .AddGoogle(AuthenticationPolicies.GoogleAuthScheme,options =>
            {
                options.ClientId = configuration["Authentication:Google:ClientId"];
                options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            });
        
        return services;
    }
}