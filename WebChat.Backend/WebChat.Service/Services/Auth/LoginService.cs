using WebChat.Service.Services.Users;
using WebChat.Shared.Common;

namespace WebChat.Service.Services.Auth;

public class LoginService : ILoginService
{
    private readonly IUserService _userService;
    
    public LoginService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<string>> Login(string email, string password)
    {
        throw new NotImplementedException();
    }
}