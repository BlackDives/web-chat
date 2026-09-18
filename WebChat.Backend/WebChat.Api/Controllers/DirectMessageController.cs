using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_api.Dtos.DirectMessages;
using web_api.Services.DirectMessages;

namespace web_api.Controllers;

[Route("v1/direct_messages")]
public class DirectMessageController : ControllerBase
{
    private readonly IDirectMessageService _directMessageService;

    public DirectMessageController(IDirectMessageService directMessageService)
    {
        _directMessageService = directMessageService;
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<List<DirectMessageChannelDTO>>> GetDirectMessageChannels([FromRoute] string id)
    {
        var userId = Guid.Parse(id);
        var result = await _directMessageService.GetDirectMessageChannelsByUserId(userId);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("channels/{id}")]
    public async Task<ActionResult<DirectMessageChannelDTO>> GetDirectMessageChannelById([FromRoute] Guid id)
    {
        var results = await _directMessageService.GetDirectMessageChannelById(id);
        if (!results.Success)
        {
            return StatusCode(StatusCodes.Status404NotFound, results.Error);
        }
        
        return Ok(results.Value);
    }

    [Authorize]
    [HttpPost("{id}")]
    public async Task<ActionResult<DirectMessageChannelDTO>> CreateNewDirectMessageChannel([FromRoute] Guid userId,
        [FromBody] DirectMessageChannelDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var results = await _directMessageService.CreateDirectMessageChannel(dto);
        if (!results.Success)
        {
            var userOneId = Guid.Parse(dto.UserOneId);
            var userTwoId = Guid.Parse(dto.UserTwoId);
            
            var result = await _directMessageService.GetDirectMessageChannelByUserIds(userOneId, userTwoId);
            if (!result.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            return Ok(result.Value);
        }

        return StatusCode(StatusCodes.Status201Created, results.Value);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<DirectMessageChannelDTO>> DeleteDirectMessageChannel([FromRoute] string id)
    {
        var parsedDMCId = Guid.Parse(id);
        var results = await _directMessageService.DeleteDirectMessageChannel(parsedDMCId);
        if (!results.Success)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return Ok(results.Value);
    }

    [Authorize]
    [HttpGet("{id}/messages")]
    public async Task<ActionResult<List<DirectMessageDTO>>> GetDirectMessagesByChannelId([FromRoute] string id)
    {
        var channelId = Guid.Parse(id);
        var results = await _directMessageService.GetDirectMessagesByChannelId(channelId);
        return Ok(results.Value);
    }

    [Authorize]
    [HttpPost("{id}/messages")]
    public async Task<ActionResult<DirectMessageDTO>> CreateNewDirectMessage([FromRoute] Guid directMessageChannelId,
        [FromBody] DirectMessageDTO directMessage)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var results = await _directMessageService.CreateDirectMessage(directMessage);
        if (!results.Success)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return StatusCode(StatusCodes.Status201Created, results.Value);
    }

    [Authorize]
    [HttpDelete("{id}/messages/{messageId}")]
    public async Task<ActionResult<DirectMessageDTO>> DeleteDirectMessage([FromRoute] string id,
        [FromRoute] string messageId)
    {
        var dmcMessageId = Guid.Parse(messageId);

        var results = await _directMessageService.DeleteDirectMessage(dmcMessageId);
        if (!results.Success)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        return Ok(results.Value);
    }
}