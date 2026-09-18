using WebChat.Infrastructure.DataAccess.Entities;
using WebChat.Shared.Models.Friendships;

namespace WebChat.Infrastructure.DataAccess.Mappers;

internal static class FriendshipMappers
{
    public static Friendship ToModel(this FriendshipEntity friendship)
    {
        return new Friendship
        {
            Id = friendship.Id,
        };
    }

    public static FriendshipEntity ToEntity(this Friendship friendship)
    {
        return new FriendshipEntity
        {
            Id = friendship.Id,
        };
    }
}