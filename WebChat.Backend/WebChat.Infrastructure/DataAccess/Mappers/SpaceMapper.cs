using WebChat.Shared.Models.Spaces;
using WebChat.Infrastructure.DataAccess.Entities;

namespace WebChat.Infrastructure.DataAccess.Mappers;

internal static class SpaceMapper
{
    public static Space ToModel(this SpaceEntity space)
    {
        return new Space
        {
            Id = space.Id,
        };
    }

    public static UserSpaceMember ToModel(this UserSpaceMemberEntity spaceMember)
    {
        return new UserSpaceMember
        {
            UserId = spaceMember.UserId,
            SpaceId = spaceMember.SpaceId,
        };
    }

    public static SpaceEntity ToEntity(this Space space)
    {
        return new SpaceEntity
        {
            Id = space.Id,
        };
    }

    public static UserSpaceMemberEntity ToEntity(this UserSpaceMember spaceMember)
    {
        return new UserSpaceMemberEntity
        {
            UserId = spaceMember.UserId,
            SpaceId = spaceMember.SpaceId,
        };
    }
}