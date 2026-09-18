using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_api.Dtos;
using web_api.Dtos.Messages;
using web_api.ServiceMessages.Channel;
using web_api.Services.Channels;
using web_api.Services.Messages;

namespace WebChat.Api.Controllers;

[Route("/v1/channels")]
public class ChannelController : ControllerBase
{
    private readonly IChannelService _channelService;
    private readonly IMessageService _messageService;
    
    public ChannelController(IChannelService  channelService, IMessageService messageService)
    {
        _channelService = channelService;
        _messageService = messageService;
    }
    
    [Authorize]
    public async Task<List<ChannelDTO>> GetServerChannels()
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ChannelDTO>> GetChannel([FromRoute] string id)
    {
        var result = await _channelService.GetChannelById(Guid.Parse(id));
        if (!result.Success)
        {
            if (result.Error.Equals(ChannelServiceErrorMessage.ChannelNotFound.Value))
            {
                return StatusCode(StatusCodes.Status404NotFound, ChannelServiceErrorMessage.ChannelNotFound.Value);
            }
        }
        
        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet("{id}/messages")]
    public async Task<ActionResult<List<MessageDTO>>> GetChannelMessagesByChannelId([FromRoute] string id)
    {
        var results = await _messageService.GetMessagesByChannelId(Guid.Parse(id));
        return Ok(results.Value);
    }

    [Authorize]
    [HttpPost("{id}/messages")]
    public async Task<ActionResult<MessageDTO>> CreateNewChannelMessage([FromBody] NewMessageDTO message,
        [FromRoute] string id)
    {
        var result = await _messageService.CreateMessage(message);
        
        return Ok(result.Value);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteChannelById([FromRoute] string id)
    {
        var channelId = Guid.Parse(id);
        var results = await _channelService.DeleteChannelById(channelId);

        if (!results.Success)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return Ok(results.Value);
    }
}