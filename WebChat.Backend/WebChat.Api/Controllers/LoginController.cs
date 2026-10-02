using Microsoft.AspNetCore.Mvc;
using web_api.Dtos.Auth;
using WebChat.Service.Services.Auth;

namespace WebChat.Api.Controllers;

[Route("/v1/auth/login")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;
    
    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLoginDto user)
    {
        throw new NotImplementedException();
    }
}