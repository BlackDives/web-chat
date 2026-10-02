using WebChat.Service.Services.Users;
using WebChat.Shared.Common;
using WebChat.Shared.Models.Auth;

namespace WebChat.Service.Services.Auth;

public class RegisterService : IRegisterService
{
    private readonly IUserService _userService;
  
    public RegisterService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<LoginUser>> CreateUser(RegisteredUser newUser)
    {
        throw new NotImplementedException();
    }
}