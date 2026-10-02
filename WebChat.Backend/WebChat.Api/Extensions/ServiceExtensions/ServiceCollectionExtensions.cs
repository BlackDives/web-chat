using System.IdentityModel.Tokens.Jwt;
using web_api.Services;
using WebChat.Infrastructure.DataAccess.Repositories.Channels;
using WebChat.Infrastructure.DataAccess.Repositories.Friendships;
using WebChat.Infrastructure.DataAccess.Repositories.Messages;
using WebChat.Infrastructure.DataAccess.Repositories.Spaces;
using WebChat.Infrastructure.DataAccess.Repositories.Users;
using WebChat.Service.Services.Auth;
using WebChat.Service.Services.Channels;
using WebChat.Service.Services.Friendships;
using WebChat.Service.Services.Messages;
using WebChat.Service.Services.Spaces;
using WebChat.Service.Services.Users;
using Webchat.Service.Services.Utils.Tokens;

namespace WebChat.Api.Extensions.ServiceExtensions;

internal static class WebChatServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IPasswordHash, PasswordHash>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ISpacesService, SpacesService>();
        services.AddScoped<IChannelService, ChannelService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IUserService,  UserService>();
        services.AddScoped<IFriendshipService, FriendshipService>();
        services.AddScoped<IGoogleAuthService, GoogleAuthService>();
        services.AddScoped<ICompleteProfileService, CompleteProfileService>();
        services.AddScoped<JwtSecurityTokenHandler>();
        
        return services;
    }

    public static IServiceCollection AddAppRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<ISpacesRepository, SpacesRepository>();
        services.AddScoped<IChannelRepository, ChannelRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IFriendshipRepository, FriendshipRepository>();
        
        return services;
    }
}