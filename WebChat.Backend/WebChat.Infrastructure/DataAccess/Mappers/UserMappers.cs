using WebChat.Infrastructure.DataAccess.Entities;
using WebChat.Shared.Models.Users;

namespace WebChat.Infrastructure.DataAccess.Mappers;

internal static class UserMappers
{
    public static User ToModel(this ApplicationUser user)
    {
        return new User
        {
            Id = user.Id,
        };
    }

    public static ApplicationUser ToEntity(this User user)
    {
        return new ApplicationUser
        {
            Id = user.Id,
        };
    }
}