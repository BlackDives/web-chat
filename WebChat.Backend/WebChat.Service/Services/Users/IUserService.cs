

using WebChat.Shared.Common;
using WebChat.Shared.Models.Auth;
using WebChat.Shared.Models.Users;

namespace WebChat.Service.Services.Users;

public interface IUserService
{
    Task<Result<User>> FindUserByIdAsync(Guid id);
    
    Task<Result<User?>> FindUserByUsernameAsync(string username);
    
    Task<Result<User>> FindUserByEmailAsync(string email);
    
    Task<Result<User>> CreateUserAsync(CompletedUserProfile newUser, string email);
}