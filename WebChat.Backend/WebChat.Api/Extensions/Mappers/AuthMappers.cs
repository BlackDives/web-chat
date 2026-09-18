using WebChat.Api.Dtos.Auth;
using WebChat.Shared.Models.Auth;

namespace WebChat.Api.Extensions.Mappers;

internal static class AuthMappers
{
    public static AuthenticatedUserDto ToDto(this AuthenticatedUser user)
    {
        return new()
        {
            AccessToken = user.AccessToken,
            RefreshToken = user.RefreshToken,
            Username = user.Username,
            Email = user.Email,
        };
    }

    public static CompleteProfileUserDto ToDto(this CompleteProfileUser user)
    {
        return new()
        {
            CompleteProfileToken = user.CompleteProfileToken,
        };
    }

    public static AuthenticatedUser ToModel(this AuthenticatedUserDto user)
    {
        return new()
        {
            AccessToken = user.AccessToken,
            RefreshToken = user.RefreshToken,
            Username = user.Username,
            Email = user.Email,
        };
    }
}