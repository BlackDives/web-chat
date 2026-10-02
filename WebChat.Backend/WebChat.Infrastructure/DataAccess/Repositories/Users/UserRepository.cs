using Microsoft.AspNetCore.Identity;
using WebChat.Infrastructure.DataAccess.Entities;
using WebChat.Shared.Models.Users;
using WebChat.Infrastructure.DataAccess.Mappers;


namespace WebChat.Infrastructure.DataAccess.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<User?> FindUserByIdAsync(Guid userId)
    {
        var query = await _userManager.FindByIdAsync(userId.ToString());
        var results = query!.ToModel();
        
        return results;
    }

    public async Task<User?> FindUserByEmailAsync(string email)
    {
        var query = await _userManager.FindByEmailAsync(email);
        var results = query?.ToModel();
        
        return results;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        var mappedUser = user.ToEntity();
        await _userManager.CreateAsync(mappedUser);

        return user;
    }
}