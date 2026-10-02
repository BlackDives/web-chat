using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebChat.Infrastructure.DataAccess;
using WebChat.Infrastructure.DataAccess.Entities;

namespace WebChat.Api.Extensions.ServiceExtensions;

internal static class PersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WebChatDbContext>(options =>
        {
            options.UseNpgsql(configuration["ConnectionStrings:PostgresDbConnection"]);
        });
        
        return services;
    }

    public static IServiceCollection AddAppIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<WebChatDbContext>();
        
        return services;
    }
}