using web_api.Services;
using web_api.Services.Auth;
using web_api.Services.Channels;
using web_api.Services.DirectMessages;
using web_api.Services.Friendship;
using web_api.Services.Messages;
using WebChat.Infrastructure.DataAccess.Repositories.Channels;
using WebChat.Infrastructure.DataAccess.Repositories.Friendships;
using WebChat.Infrastructure.DataAccess.Repositories.Messages;
using WebChat.Infrastructure.DataAccess.Repositories.Spaces;
using WebChat.Infrastructure.DataAccess.Repositories.Users;
using WebChat.Service.Services.Auth;
using WebChat.Service.Services.Servers;
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
        services.AddScoped<IServersService, ServersService>();
        services.AddScoped<IChannelService, ChannelService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IUserService,  UserService>();
        services.AddScoped<IFriendshipService, FriendshipService>();
        services.AddScoped<IDirectMessageService, DirectMessagesService>();
        services.AddScoped<IGoogleAuthService, GoogleAuthService>();
        
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