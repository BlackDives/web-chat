using web_api.Dtos.Auth;
using web_api.Utils;

namespace web_api.Services.Auth;

public interface IRegisterService
{
    Task<Result<string>> CreateUser(UserRegisterDto userRegisterDto);
}