using WebChat.Shared.Models.Users;

namespace WebChat.Infrastructure.DataAccess.Repositories.Users;

public interface IUserRepository
{
    Task<User?> FindUserByIdAsync(Guid userId);
    Task<User?> FindUserByEmailAsync(string email);
    Task<User?> FindUserByUsernameAsync(string username);
    Task<User> CreateUserAsync(User user);
}