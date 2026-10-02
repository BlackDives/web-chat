using Microsoft.AspNetCore.Mvc;
using web_api.Dtos.Auth;
using WebChat.Service.Services.Auth;

namespace Webchat.Api.Controllers;

[ApiController]
[Route("v1/auth/register")]
public class RegisterController : ControllerBase
{
    private readonly IRegisterService _registerService;
    
    public RegisterController(IRegisterService registerService)
    {
        _registerService = registerService;
    }
    
    [HttpPost]
    public async Task<ActionResult<string>> RegisterUser([FromBody] UserRegisterDto newUserDto)
    {
        throw new NotImplementedException();
    }
}