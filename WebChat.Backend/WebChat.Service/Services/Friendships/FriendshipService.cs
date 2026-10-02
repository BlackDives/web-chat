

using WebChat.Infrastructure.DataAccess.Repositories.Friendships;
using WebChat.Service.Services.Users;
using WebChat.Shared.Common;
using WebChat.Shared.Models.Friendships;

namespace WebChat.Service.Services.Friendships;

public class FriendshipService : IFriendshipService
{
    private readonly IFriendshipRepository _friendshipRepository;
    private readonly IUserService _userService;
    
    public FriendshipService(IFriendshipRepository friendshipRepository, IUserService userService)
    {
        _friendshipRepository = friendshipRepository;
        _userService = userService;
    }
    
    public async Task<Result<Friendship>> CreateFriendRequestAsync(FriendshipToCreate friendshipRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Friendship>> AcceptFriendRequestAsync(Guid senderId, Guid receiverId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> DeclineReceivedFriendRequestAsync(Guid receiverId, Guid senderId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> CancelSentFriendRequestAsync(Guid senderId, Guid receiverId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<Friendship>>> GetSentFriendRequestsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<Friendship>>> GetReceivedFriendRequestsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<Friendship>>> GetFriendsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> RemoveFriendshipAsync(Guid userId, Guid friendId)
    {
        throw new NotImplementedException();
    }
}