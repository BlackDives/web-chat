using WebChat.Infrastructure.DataAccess.Entities;
using WebChat.Shared.Models.Auth;

namespace WebChat.Infrastructure.DataAccess.Mappers;

internal static class AuthMappers
{
    public static RefreshToken ToModel(this RefreshTokenEntity model)
    {
        return new RefreshToken
        {
            Id = model.Id,
            Token = model.Token,
        };
    }

    public static RefreshTokenEntity ToEntity(this RefreshToken model)
    {
        return new RefreshTokenEntity
        {
            Id = model.Id,
            Token = model.Token,
        };
    }
}