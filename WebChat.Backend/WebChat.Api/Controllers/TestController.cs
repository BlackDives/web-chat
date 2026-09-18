using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.Data;
using web_api.DataAccess.Users;
using web_api.Services;

namespace web_api.Controllers;

[Route("admin/rehash")]
public class TestController : ControllerBase
{
    private readonly IUsersRepository _usersRepository;
    private readonly ApplicationDbContext _dbContext;
    private readonly IPasswordHash _passwordHasher;
    
    public TestController(IUsersRepository usersRepository, ApplicationDbContext dbContext, IPasswordHash passwordHasher)
    {
        _usersRepository = usersRepository;
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }
    [HttpPost("{id}")]
    public async Task<ActionResult> Rehash([FromBody] ResetPasswordDTO password, [FromRoute] string id)
    {
        var userId = Guid.Parse(id);
        var getUser = await _dbContext.ApplicationUsers.Where(u => u.Id == userId).SingleAsync();
        var oldPw = getUser.Password;
        var newPw = _passwordHasher.HashPassword(password.Password);
        getUser.Password = newPw;
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}

public class ResetPasswordDTO
{
    public string Password { get; set; }
}