using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_api.Dtos;
using web_api.Dtos.Messages;
using WebChat.Service.Services.Channels;
using WebChat.Service.Services.Messages;

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
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpGet("{id}/messages")]
    public async Task<ActionResult<List<MessageDTO>>> GetChannelMessagesByChannelId([FromRoute] string id)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPost("{id}/messages")]
    public async Task<ActionResult<MessageDTO>> CreateNewChannelMessage([FromBody] NewMessageDTO message,
        [FromRoute] string id)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteChannelById([FromRoute] string id)
    {
        throw new NotImplementedException();
    }
}