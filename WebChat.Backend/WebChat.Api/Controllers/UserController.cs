using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_api.Dtos;
using web_api.Services.Users;
using WebChat.Api.Policies;
using WebChat.Service.Services.Servers;

namespace WebChat.Api.Controllers;

[Route("users")]
public class UserController : ControllerBase
{
    private readonly IServersService _serverService;
    private readonly IUserService _userService;
    
    public UserController(IServersService serverService, IUserService userService)
    {
        _serverService = serverService;
        _userService = userService;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = $"{AuthenticationPolicies.CompleteProfileScheme}")]
    public async Task<IActionResult> CreateNewUser()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}/servers")]
    [Authorize]
    public async Task<List<ServerDto>> GetUserServers(string id)
    {
        var servers = await _serverService.GetServersByUserId(Guid.Parse(id));
        return servers.Value;
    }

    [Authorize]
    [HttpPost("{id}/direction-messages")]
    public async Task GetUserDirectMessages()
    {
        
    }

    public async Task GetUserFriends()
    {
        
    }
}