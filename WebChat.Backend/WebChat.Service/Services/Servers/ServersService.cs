using web_api.Data;
using web_api.DataAccess.Servers;
using web_api.DataAccess.Users;
using web_api.DataAccess.UserServerMembers;
using web_api.Dtos;
using web_api.Dtos.User;
using web_api.ServiceMessages.Server;
using web_api.Services.Channels;
using web_api.Services.Messages;
using web_api.Utils;

namespace WebChat.Service.Services.Servers;
public class ServersService : IServersService
{
    private readonly IServersRepository _serversRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IUserServerMemberRepository _userServerMemberRepository;
    private readonly IMessageService _messageService;
    private readonly IChannelService _channelService;

    public ServersService(IServersRepository serversRepository, IUsersRepository usersRepository, IUserServerMemberRepository userServerMemberRepository,  IMessageService messageService, IChannelService channelService)
    {
        _serversRepository = serversRepository;
        _usersRepository = usersRepository;
        _userServerMemberRepository = userServerMemberRepository;
        _messageService = messageService;
        _channelService = channelService;
    }
    
    public Task<List<ServerDto>> GetServersByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<Space> CreateServer(ServerDto server, string userId)
    {
        var newServer = new Space
        {
            Id = Guid.NewGuid(),
            ServerName = server.Name,
            ServerOwnerId = Guid.Parse(userId),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        
        var results = await _serversRepository.CreateServer(newServer);

        return results;
    }

    public async Task<Result<List<ServerDto>>> GetServersByUserId(Guid userId)
    {
        var results = await _serversRepository.GetServersOwnedById(userId);
        var mappedResults = new List<ServerDto>();

        foreach (var server in results)
        {
            var serverMap = new ServerDto
            {
                Id = server.Id.ToString(),
                ServerOwnerId = server.ServerOwnerId.ToString(),
                Name = server.ServerName
            };
            
            mappedResults.Add(serverMap);
        }
        return Result<List<ServerDto>>.Ok(mappedResults);
    }

    public async Task<Result<ServerInviteDTO>> InviteUserToServerByUsername(string serverId, string username)
    {
        var user = await _usersRepository.GetUserByUsername(username);
        var parsedServerId = Guid.Parse(serverId);
        if (user == null)
        {
            return Result<ServerInviteDTO>.Fail(ServerServiceErrorMessages.UserNotFound.Value);
        }

        var checkUserMembership = await _userServerMemberRepository.GetSinglServerMembershipByUserId(parsedServerId, user.Id);
        if (checkUserMembership != null)
        {
            return Result<ServerInviteDTO>.Fail(ServerServiceErrorMessages.UserAlreadyServerMember.Value);
        }

        var results = await _serversRepository.AddUserToServerByUserId(parsedServerId, user.Id);

        var newDto = new ServerInviteDTO
        {
            Username = user.Id.ToString()
        };

        return Result<ServerInviteDTO>.Ok(newDto);
    }

    public async Task<Result<List<UserDTO>>> GetUsersByServerId(Guid serverId)
    {
        var results = await _serversRepository.GetUsersByServerId(serverId);
        var users = new List<ApplicationUser>();
        var endResults = new List<UserDTO>();

        foreach (UserServerMember user in results)
        {
            var serverUser = await _usersRepository.GetUserById(user.UserId);
            users.Add(serverUser);
        }

    
        foreach (UserServerMember user in results)
        {
            var theUser = users.Find(x => x.Id == user.UserId);
            var finalUser = new UserDTO
            {
                Id = theUser.Id.ToString(),
                Username = theUser.UserName
            };
            
            endResults.Add(finalUser);
        }

        return Result<List<UserDTO>>.Ok(endResults);
    }

    public async Task<Result<UserDTO>> RemoveUserFromServerByUserId(Guid serverId, Guid userId)
    {
        var checkMembership = await _userServerMemberRepository.GetSinglServerMembershipByUserId(serverId, userId);
        if (checkMembership == null)
        {
            return Result<UserDTO>.Fail(ServerServiceErrorMessages.UserNotServerMember.Value);
        }

        var getUser = await _usersRepository.GetUserById(userId);
        if (getUser == null)
        {
            return Result<UserDTO>.Fail(ServerServiceErrorMessages.UserNotFound.Value);
        }
        var results = await _serversRepository.RemoveUserFromServerByUserId(serverId, userId);
        var finalResults = new UserDTO
        {
            Id = getUser.Id.ToString(),
            Username = getUser.UserName
        };
        
        return Result<UserDTO>.Ok(finalResults);
    }

    public async Task<Result<ServerDto>> GetServerById(Guid id)
    {
        var  server = await _serversRepository.GetServerById(id);
        if (server == null)
        {
            return Result<ServerDto>.Fail("Server Not Found");
        }

        var mappedServer = new ServerDto
        {
            Id = server.Id.ToString(),
            ServerOwnerId = server.ServerOwnerId.ToString(),
            Name = server.ServerName
        };
        
        return Result<ServerDto>.Ok(mappedServer);
    }

    public async Task<Result<ServerDto>> DeleteServerById(Guid serverId)
    {
        var checkServerExistence = await _serversRepository.GetServerById(serverId);
        if (checkServerExistence != null)
        {
            var serverChannels = await _channelService.GetChannelsByServerId(serverId);
            foreach (var serverChannel in serverChannels)
            {
                var channelId = Guid.Parse(serverChannel.Id);
                await _channelService.RemoveChannelMessagesByChannelId(channelId);
                await _channelService.DeleteChannelById(channelId);
            }

            await _serversRepository.DeleteServerById(serverId);
            var mappedServer = new ServerDto
            {
                Id = serverId.ToString(),
                ServerOwnerId = checkServerExistence.ServerOwnerId.ToString(),
                Name = serverId.ToString()
            };
            
            return Result<ServerDto>.Ok(mappedServer);
        }
        
        return Result<ServerDto>.Fail("Server not found.");
    }
}