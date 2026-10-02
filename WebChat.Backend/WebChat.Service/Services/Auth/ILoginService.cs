using WebChat.Shared.Common;

namespace WebChat.Service.Services.Auth;

public interface ILoginService
{
    public Task<Result<string>> Login(string email, string password);
}