using web_api.Dtos;
using web_api.Data;
using web_api.Dtos.User;
using web_api.Utils;
using WebChat.Shared.Common;
using WebChat.Shared.Models;

namespace WebChat.Service.Services.Servers;

public interface IServersService
{
    Task<List<ServerDto>> GetServersByEmail(string email);
    
    Task<Result<List<ServerDto>>> GetServersByUserId(Guid userId);
    Task<Result<ServerDto>> GetServerById(Guid id);
    
    Task<Result<List<UserDTO>>> GetUsersByServerId(Guid serverId);
    Task<Space> CreateServer(ServerDto server, string userId);
    
    Task<Result<ServerInviteDTO>> InviteUserToServerByUsername(string serverId, string username);
    
    Task<Result<UserDTO>> RemoveUserFromServerByUserId(Guid serverId, Guid userId);
    
    Task<Result<ServerDto>> DeleteServerById(Guid serverId);
}