using WebChat.Shared.Common;
using WebChat.Shared.Models.Friendships;

namespace WebChat.Service.Services.Friendships;

public interface IFriendshipService
{
    Task<Result<Friendship>> CreateFriendRequestAsync(FriendshipToCreate friendshipRequest);
    Task<Result<Friendship>> AcceptFriendRequestAsync(Guid senderId, Guid receiverId);
    Task<Result<bool>> DeclineReceivedFriendRequestAsync(Guid receiverId, Guid senderId);
    Task<Result<bool>> CancelSentFriendRequestAsync(Guid senderId, Guid receiverId);
    Task<Result<List<Friendship>>> GetSentFriendRequestsAsync(Guid userId);
    Task<Result<List<Friendship>>> GetReceivedFriendRequestsAsync(Guid userId);
    Task<Result<List<Friendship>>> GetFriendsAsync(Guid userId);
    Task<Result<bool>> RemoveFriendshipAsync(Guid userId, Guid friendId);
}