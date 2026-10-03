using WebChat.Shared.Common;
using WebChat.Shared.Models.Auth;

namespace WebChat.Service.Services.Auth;

public interface IRegisterService
{
    Task<Result<LoginUser>> CreateUser(RegisteredUser newUser);
}