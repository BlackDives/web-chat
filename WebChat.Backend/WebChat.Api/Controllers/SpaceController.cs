using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_api.Dtos;
using web_api.Dtos.User;
using web_api.ServiceMessages.Server;
using WebChat.Infrastructure.DataAccess.Repositories.Spaces;
using WebChat.Service.Services.Channels;
using WebChat.Service.Services.Spaces;

namespace WebChat.Api.Controllers;

[Route("spaces")]
public class ServerController : ControllerBase
{
    private readonly ISpacesService _serversService;
    public ServerController(ISpacesService serversService)
    {
        _serversService = serversService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetServersByUser()
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ServerDto>> GetServer([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpGet("{serverId}/channels")]
    public async Task<List<ChannelDTO>> GetServerChannels([FromRoute] string serverId)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPost("{id}/channels")]
    public async Task<IActionResult> CreateServerChannel([FromBody] ChannelDTO channel, string id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}/channels/{channelId}")]
    [Authorize]
    public async Task<ChannelDTO> GetServerChannel([FromRoute] string channelId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}/members")]
    [Authorize]
    public async Task<List<UserDTO>> GetServerMembers([FromRoute] string id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateServer([FromBody] ServerDto server)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPost("{id}/invite")]
    public async Task<IActionResult> InviteUserToServer([FromBody] ServerInviteDTO serverInviteCredentials, [FromRoute] string id)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpDelete("{id}/members/{userId}")]
    public async Task<IActionResult> DeleteServerMember([FromRoute] string id, [FromRoute] string userId)
    {
        throw new NotImplementedException();
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServerDto>> DeleteServer([FromRoute] string id)
    {
        throw new NotImplementedException();
    }
}