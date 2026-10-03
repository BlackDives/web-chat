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
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            ProfilePictureUrl = user.ProfilePictureUrl,
            EnabledNotifications = user.EnableNotifications,
            DateOfBirth = user.DateOfBirth,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
        };
    }

    public static ApplicationUser ToEntity(this User user)
    {
        return new ApplicationUser
        {
            Id = user.Id,
            UserName = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            ProfilePictureUrl = user.ProfilePictureUrl,
            EnableNotifications = user.EnabledNotifications,
            DateOfBirth = user.DateOfBirth,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
        };
    }
}