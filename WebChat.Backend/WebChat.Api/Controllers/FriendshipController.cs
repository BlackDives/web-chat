using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using web_api.Dtos.Friendship;
using web_api.Services.Friendship;

namespace web_api.Controllers;

[Route("v1/friendships")]
public class FriendshipController : ControllerBase
{
    private readonly IFriendshipService _friendshipService;
    
    public FriendshipController(IFriendshipService friendshipService)
    {
        _friendshipService = friendshipService;
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<FriendshipRequestDTO>> NewFriendshipRequest([FromBody] FriendshipRequestDTO request)
    {
        var results = await _friendshipService.CreateFriendRequest(request);

        if (!results.Success)
        {
            if (results.Error.Equals("User not found."))
            {
                return StatusCode(StatusCodes.Status404NotFound, results.Error);
            }
        }

        return results.Value;
    }

    [Authorize]
    [HttpGet("{id}/requests-received")]
    public async Task<ActionResult<List<FriendshipRequestDTO>>> GetFriendshipRequests([FromRoute] string id)
    {
        var results = await _friendshipService.GetReceivedFriendRequests(Guid.Parse(id));
        
        return results.Value;
    }

    [Authorize]
    [HttpGet("{id}/requests-sent")]
    public async Task<ActionResult<List<FriendshipRequestDTO>>> GetSentFriendRequests([FromRoute] string id)
    {
        var results = await _friendshipService.GetSentFriendRequests(Guid.Parse(id));
        
        return results.Value;
    }

    [Authorize]
    [HttpPost("{id}/accept-request")]
    public async Task<ActionResult<FriendshipRequestDTO>> AcceptFriendshipRequest([FromRoute] string id, [FromBody] FriendshipRequestDTO request)
    {
        var result = await _friendshipService.AcceptFriendRequest(Guid.Parse(id), request);
        if (!result.Success)
        {
            return StatusCode(StatusCodes.Status409Conflict, result.Error);
        }

        return Ok(result.Value);
    }
    
    [Authorize]
    [HttpPost("{id}/reject-request")]
    public async Task<ActionResult<FriendshipRequestDTO>> DeclineFriendshipRequest([FromRoute] string id, [FromBody] FriendshipRequestDTO request)
    {
        var result = await _friendshipService.DeclineReceivedFriendRequest(Guid.Parse(id), request);
        if (!result.Success)
        {
            return StatusCode(StatusCodes.Status409Conflict, result.Error);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPost("{id}/cancel-request")]
    public async Task<ActionResult<FriendshipRequestDTO>> CancelFriendshipRequest([FromRoute] string id,  [FromBody] FriendshipRequestDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var results = await _friendshipService.CancelSentFriendRequest(request);
        if (!results.Success)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        return Ok(results.Value);
    }
    
    [Authorize]
    [HttpGet("{id}/friends")]
    public async Task<ActionResult<List<FriendshipDTO>>> GetFriends([FromRoute] string id)
    {
        var results = await _friendshipService.GetFriends(Guid.Parse(id));
        return results.Value;
    }

    [Authorize]
    [HttpPost("{id}/remove-friend")]
    public async Task<ActionResult> RemoveFriend([FromRoute] string id, [FromBody] FriendshipDTO friendshipDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var results = await _friendshipService.RemoveFriendship(friendshipDTO);
        if (!results.Success)
        {
            return StatusCode(StatusCodes.Status409Conflict, results.Error);
        }

        return Ok(results);
    }
    
}