using Microsoft.AspNetCore.Mvc;
using web_api.Data;
using web_api.Dtos.Auth;
using web_api.Services.Auth;

namespace web_api.Controllers;

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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var token = await _loginService.Login(user.Email, user.Password);

        if (!token.Success)
        {
            if (token.Error.Equals("User not found.") || token.Error.Equals("Invalid password."))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Email or password is incorrect.");
            } 
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        return Ok(token.Value);
    }
}