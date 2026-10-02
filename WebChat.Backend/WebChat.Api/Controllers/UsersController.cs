using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebChat.Api.Controllers;

[Route("api/users")]
public class UsersController : ControllerBase
{
    public UsersController()
    {
        
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult GetCurrentUser()
    {
        /*
         * 1. Get access and refresh token
         * 2. check expiry
         * 3. if both existing and not expired -> return user details
         * 4. if both existing and access token expired -> reroute to refresh token controller
         * 5. if both existing and expired -> return a 401
         */
        
        throw new NotImplementedException();
    }
}