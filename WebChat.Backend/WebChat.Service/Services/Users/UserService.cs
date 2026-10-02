using WebChat.Infrastructure.DataAccess.Repositories.Users;
using WebChat.Shared.Common;
using WebChat.Shared.Enums;
using WebChat.Shared.Models.Auth;
using WebChat.Shared.Models.Users;

namespace WebChat.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _usersRepository;
    
    public UserService(IUserRepository  usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public async Task<Result<User>> FindUserByIdAsync(Guid id)
    {
        var user = await _usersRepository.FindUserByIdAsync(id);
        
        if (user == null)
        {
            return Result<User>.Fail(ResultMessage.UserNotFound, ServiceErrorEnum.NotFound);
        }

        return Result<User>.Ok(user);
    }

    public async Task<Result<User>> FindUserByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<User>> FindUserByEmailAsync(string email)
    {
        var user = await _usersRepository.FindUserByEmailAsync(email);
        if (user == null)
        {
            return Result<User>.Fail(ResultMessage.UserNotFound, ServiceErrorEnum.NotFound);
        }
        
        return Result<User>.Ok(user);
    }

    public async Task<Result<User>> CreateUserAsync(CompletedUserProfile newUser, string email)
    {
        var userToAdd = new User
        {
            Id = Guid.NewGuid(),
            Username = newUser.Username,
            DisplayName = newUser.Username,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Email = email,
            PhoneNumber = null,
            ProfilePictureUrl = null,
            EnabledNotifications = false,
            DateOfBirth = null,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
        };
        
        var result = await _usersRepository.CreateUserAsync(userToAdd);
        
        return Result<User>.Ok(result);
    }
}