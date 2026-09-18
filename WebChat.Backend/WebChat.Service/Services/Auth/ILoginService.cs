using web_api.Utils;

namespace web_api.Services.Auth;

public interface ILoginService
{
    public Task<Result<string>> Login(string email, string password);
}