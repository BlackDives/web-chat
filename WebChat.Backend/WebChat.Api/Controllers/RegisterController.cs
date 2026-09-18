using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web_api.Data;
using web_api.Dtos.Auth;
using web_api.Services.Auth;

namespace web_api.Controllers;

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
        if (!ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }
        
        var token = await _registerService.CreateUser(newUserDto);
        if (!token.Success)
        {
            if (token.Error.Equals("User with email already exists."))
            {
                return StatusCode(StatusCodes.Status409Conflict, token.Error);
            }
            else if (token.Error.Equals("User with username already exists."))
            {
                return StatusCode(StatusCodes.Status409Conflict, token.Error);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        return Ok(token.Value);
    }
}