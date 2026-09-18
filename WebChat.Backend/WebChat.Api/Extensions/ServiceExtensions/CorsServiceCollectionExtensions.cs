using WebChat.Api.Policies;

namespace WebChat.Api.Extensions.ServiceExtensions;

internal static class CorsServiceCollectionExtensions
{
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicies.DevelopmentCorsPolicy, policy =>
            {
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
            
            options.AddPolicy(CorsPolicies.WebChatCorsPolicy, policy =>
            {
                policy.WithOrigins("https://test.webchat.com");
            });
        });
        
        return services;
    }
}