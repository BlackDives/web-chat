using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_api.DataAccess.Servers;
using web_api.Dtos;
using web_api.Dtos.User;
using web_api.ServiceMessages.Server;
using web_api.Services.Channels;
using web_api.Services.Servers;

namespace web_api.Controllers;

[Route("/v1/servers")]
public class ServerController : ControllerBase
{
    private readonly IServersService _serversService;
    private readonly IChannelService _channelService;
    
    public ServerController(IServersService serversService, IChannelService  channelsService)
    {
        _serversService = serversService;
        _channelService = channelsService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetServersByUser()
    {
        return null;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ServerDto>> GetServer([FromRoute] string id)
    {
        var serverId = Guid.Parse(id);
        var results = await _serversService.GetServerById(serverId);
        if (!results.Success)
        {
            if (results.Error.Equals("Server Not Found"))
            {
                return NotFound(results.Error);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, results.Error);
            }
        }
        return Ok(results.Value);
    }

    [Authorize]
    [HttpGet("{serverId}/channels")]
    public async Task<List<ChannelDTO>> GetServerChannels([FromRoute] string serverId)
    {
        var parsedServerId = Guid.Parse(serverId);
        var results = await _channelService.GetChannelsByServerId(parsedServerId);

        return results;
    }

    [Authorize]
    [HttpPost("{id}/channels")]
    public async Task<IActionResult> CreateServerChannel([FromBody] ChannelDTO channel, string id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var serverId = Guid.Parse(id);
        
        var results = await _channelService.AddServerChannel(serverId, channel);
        if (results == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return Ok(results);
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
        var query = await _serversService.GetUsersByServerId(Guid.Parse(id));
        var results = query.Value;
        
        return results;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateServer([FromBody] ServerDto server)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var claims = User.Claims.ToList();
        var userId = claims.Where(x => x.Type.Contains("UserId")).Select(x => x.Value).FirstOrDefault();
        
        var result = await _serversService.CreateServer(server, userId);

        if (result == null)
        {
            return StatusCode(500);
        }
        

        return Ok(result);
    }

    [Authorize]
    [HttpPost("{id}/invite")]
    public async Task<IActionResult> InviteUserToServer([FromBody] ServerInviteDTO serverInviteCredentials, [FromRoute] string id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var results = await _serversService.InviteUserToServerByUsername(id, serverInviteCredentials.Username);
        if (!results.Success)
        {
            if (results.Error.Equals(ServerServiceErrorMessages.UserNotFound.Value))
            {
                return StatusCode(StatusCodes.Status404NotFound, results.Error);
            }
            else if (results.Error.Equals(ServerServiceErrorMessages.UserAlreadyServerMember.Value))
            {
                return StatusCode(StatusCodes.Status409Conflict, results.Error);
            }
        }
        
        return Ok(results);
    }

    [Authorize]
    [HttpDelete("{id}/members/{userId}")]
    public async Task<IActionResult> DeleteServerMember([FromRoute] string id, [FromRoute] string userId)
    {
        var results = await _serversService.RemoveUserFromServerByUserId(Guid.Parse(id), Guid.Parse(userId));
        
        if (!results.Success)
        {
            if (results.Error.Equals(ServerServiceErrorMessages.UserNotFound.Value))
            {
                return StatusCode(StatusCodes.Status404NotFound, results.Error);
            }
            else if (results.Error.Equals(ServerServiceErrorMessages.UserNotServerMember.Value))
            {
                return StatusCode(StatusCodes.Status409Conflict, results.Error);
            }
        }

        return Ok(results.Value);
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServerDto>> DeleteServer([FromRoute] string id)
    {
        var serverId = Guid.Parse(id);
        var results = await _serversService.DeleteServerById(serverId);
        if (!results.Success)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return Ok(results);
    }
}