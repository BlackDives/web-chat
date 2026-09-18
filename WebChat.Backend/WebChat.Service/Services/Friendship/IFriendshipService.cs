using web_api.Dtos.Friendship;
using web_api.Utils;

namespace web_api.Services.Friendship;

public interface IFriendshipService
{
    Task<Result<FriendshipRequestDTO>> CreateFriendRequest(FriendshipRequestDTO friendshipRequestDTO);
    Task<Result<FriendshipDTO>> AcceptFriendRequest(Guid userId, FriendshipRequestDTO friendshipRequestDTO);
    Task<Result<FriendshipRequestDTO>> DeclineReceivedFriendRequest(Guid userId, FriendshipRequestDTO friendshipRequestDTO);
    Task<Result<FriendshipRequestDTO>> CancelSentFriendRequest(FriendshipRequestDTO friendshipRequestDTO);
    Task<Result<List<FriendshipRequestDTO>>> GetSentFriendRequests(Guid userId);
    Task<Result<List<FriendshipRequestDTO>>> GetReceivedFriendRequests(Guid userId);
    Task<Result<List<FriendshipDTO>>> GetFriends(Guid userId);
    
    Task<Result<FriendshipDTO>> RemoveFriendship(FriendshipDTO friendshipDTO);
}