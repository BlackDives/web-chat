using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using web_api.Dtos.Friendship;
using WebChat.Service.Services.Friendships;

namespace WebChat.Api.Controllers;

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
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpGet("{id}/requests-received")]
    public async Task<ActionResult<List<FriendshipRequestDTO>>> GetFriendshipRequests([FromRoute] string id)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpGet("{id}/requests-sent")]
    public async Task<ActionResult<List<FriendshipRequestDTO>>> GetSentFriendRequests([FromRoute] string id)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPost("{id}/accept-request")]
    public async Task<ActionResult<FriendshipRequestDTO>> AcceptFriendshipRequest([FromRoute] string id, [FromBody] FriendshipRequestDTO request)
    {
        throw new NotImplementedException();
    }
    
    [Authorize]
    [HttpPost("{id}/reject-request")]
    public async Task<ActionResult<FriendshipRequestDTO>> DeclineFriendshipRequest([FromRoute] string id, [FromBody] FriendshipRequestDTO request)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPost("{id}/cancel-request")]
    public async Task<ActionResult<FriendshipRequestDTO>> CancelFriendshipRequest([FromRoute] string id,  [FromBody] FriendshipRequestDTO request)
    {
        throw new NotImplementedException();
    }
    
    [Authorize]
    [HttpGet("{id}/friends")]
    public async Task<ActionResult<List<FriendshipDTO>>> GetFriends([FromRoute] string id)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPost("{id}/remove-friend")]
    public async Task<ActionResult> RemoveFriend([FromRoute] string id, [FromBody] FriendshipDTO friendshipDTO)
    {
        throw new NotImplementedException();
    }
    
}