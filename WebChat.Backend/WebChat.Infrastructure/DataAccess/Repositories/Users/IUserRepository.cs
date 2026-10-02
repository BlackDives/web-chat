using WebChat.Shared.Models.Users;

namespace WebChat.Infrastructure.DataAccess.Repositories.Users;

public interface IUserRepository
{
    Task<User?> FindUserByIdAsync(Guid userId);
    Task<User?> FindUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user);
}