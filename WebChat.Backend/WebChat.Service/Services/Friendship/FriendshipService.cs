using web_api.Data;
using web_api.DataAccess.Friendship;
using web_api.Dtos.Friendship;
using web_api.Services.Users;
using web_api.Utils;

namespace web_api.Services.Friendship;

public class FriendshipService : IFriendshipService
{
    private readonly IUserService _userService;
    private readonly IFriendshipRepository _friendshipRepository;
    
    public FriendshipService(IFriendshipRepository friendshipRepository, IUserService userService)
    {
        _userService = userService;
        _friendshipRepository = friendshipRepository;
    }
    
    public async Task<Result<FriendshipRequestDTO>> CreateFriendRequest(FriendshipRequestDTO friendshipRequestDTO)
    {
        var userId = Guid.Parse(friendshipRequestDTO.RequesterId);
        var checkRequestedUserExistence = await _userService.GetUserByUsername(friendshipRequestDTO.RequestedName);
        var getReceivedRequests = await _friendshipRepository.GetReceivedFriendRequests(userId);
        var getSentRequests = await _friendshipRepository.GetSentFriendRequests(userId);
        
        if (!checkRequestedUserExistence.Success)
        {
            return Result<FriendshipRequestDTO>.Fail("User not found.");
        }

        var checkRequestExistence = getReceivedRequests.Where(r => r.RequestingUserId == userId).SingleOrDefault();
        if (checkRequestExistence != null)
        {
            return Result<FriendshipRequestDTO>.Fail("Request already exists.");
        }
        
        var checkReceivedRequestExistence = getSentRequests.Where(r => r.RequestedUserId == Guid.Parse(checkRequestedUserExistence.Value.Id)).SingleOrDefault();
        if (checkReceivedRequestExistence != null)
        {
            var newFriends = new Friends
            {
                FriendOneId = userId,
                FriendTwoId = Guid.Parse(checkRequestedUserExistence.Value.Id),
                FriendsOn = DateTime.UtcNow
            };
            var newFriendshipQuery = await _friendshipRepository.CreateFriendship(newFriends);
            return Result<FriendshipRequestDTO>.Ok(friendshipRequestDTO);
        }

        var requestedUser = checkRequestedUserExistence.Value;
        var friendRequest = new FriendRequest
        {
            RequestedUserId = Guid.Parse(requestedUser.Id),
            RequestingUserId = Guid.Parse(friendshipRequestDTO.RequesterId),
            RequestedOn = DateTime.UtcNow
        };
        
        var results = await _friendshipRepository.CreateFriendRequest(friendRequest);
        
        return Result<FriendshipRequestDTO>.Ok(friendshipRequestDTO);
    }

    public async Task<Result<List<FriendshipRequestDTO>>> GetSentFriendRequests(Guid userId)
    {
        var friendRequests = await _friendshipRepository.GetSentFriendRequests(userId);
        var results = new List<FriendshipRequestDTO>();
        
        foreach (var friendRequest in friendRequests)
        {
            var requestedUser = await _userService.GetUserById(friendRequest.RequestedUserId);
            var requestingUser = await _userService.GetUserById(friendRequest.RequestingUserId);

            var mappedFriendRequest = new FriendshipRequestDTO
            {
                RequestedName = requestedUser.Value.Username,
                RequesterName = requestingUser.Value.Username,
                RequesterId = requestingUser.Value.Id,
            };
            results.Add(mappedFriendRequest);
        }
        
        return Result<List<FriendshipRequestDTO>>.Ok(results);
    }

    public async Task<Result<List<FriendshipRequestDTO>>> GetReceivedFriendRequests(Guid userId)
    {
        var friendRequests = await _friendshipRepository.GetReceivedFriendRequests(userId);
        var results = new List<FriendshipRequestDTO>();

        foreach (var friendRequest in friendRequests)
        {
            var requestedUser = await _userService.GetUserById(friendRequest.RequestedUserId);
            var requestingUser = await _userService.GetUserById(friendRequest.RequestingUserId);

            var mappedFriendRequest = new FriendshipRequestDTO
            {
                RequestedName = requestedUser.Value.Username,
                RequesterName = requestingUser.Value.Username,
                RequesterId = requestingUser.Value.Id,
            };
            
            results.Add(mappedFriendRequest);
        }
        
        return Result<List<FriendshipRequestDTO>>.Ok(results);
    }

    public async Task<Result<FriendshipDTO>> AcceptFriendRequest(Guid userId, FriendshipRequestDTO friendshipRequestDTO)
    {
        var getReceivedRequests = await _friendshipRepository.GetReceivedFriendRequests(userId);
        var request = getReceivedRequests.Where(r => r.RequestingUserId == Guid.Parse(friendshipRequestDTO.RequesterId)).SingleOrDefault();
        var newFriendship = new Friends
        {
            FriendOneId = request.RequestedUserId,
            FriendTwoId = request.RequestingUserId,
            FriendsOn = DateTime.UtcNow
        };
        
        var results = await _friendshipRepository.CreateFriendship(newFriendship);
        var removedRequest = await _friendshipRepository.RemoveFriendRequest(request);
        var userOne = await _userService.GetUserById(results.FriendOneId);
        var userTwo = await _userService.GetUserById(results.FriendTwoId);

        var mappedResults = new FriendshipDTO
        {
            FriendOneId = userOne.Value.Id,
            FriendOneName = userOne.Value.Username,
            FriendTwoId = userTwo.Value.Id,
            FriendTwoName = userTwo.Value.Username,
            CreatedAt = results.FriendsOn
        };
        
        return Result<FriendshipDTO>.Ok(mappedResults);
    }

    public async Task<Result<FriendshipRequestDTO>> DeclineReceivedFriendRequest(Guid userId, FriendshipRequestDTO friendshipRequestDTO)
    {
        var getReceivedRequest = await _friendshipRepository.GetReceivedFriendRequests(userId);
        var request = getReceivedRequest.Where(r => r.RequestingUserId == Guid.Parse(friendshipRequestDTO.RequesterId))
            .SingleOrDefault();
        if (request == null)
        {
            return Result<FriendshipRequestDTO>.Fail("Request not found.");
        }

        var results = _friendshipRepository.RemoveFriendRequest(request);

        return Result<FriendshipRequestDTO>.Ok(friendshipRequestDTO);
    }

    public async Task<Result<FriendshipRequestDTO>> CancelSentFriendRequest(FriendshipRequestDTO friendshipRequestDTO)
    {
        var userId = Guid.Parse(friendshipRequestDTO.RequesterId);
        var user = await _userService.GetUserById(userId);
        var getSentRequests = await _friendshipRepository.GetSentFriendRequests(userId);
        var sentStuff = new List<FriendshipRequestDTO>();
        
        foreach (var sentRequest in getSentRequests)
        {
            var requestedUser = await _userService.GetUserById(sentRequest.RequestedUserId);
            var STFU = new FriendshipRequestDTO
            {
                RequestedName = requestedUser.Value.Username,
                RequesterName = user.Value.Username,
                RequesterId = user.Value.Id,
                RequestedId = requestedUser.Value.Id
            };
            sentStuff.Add(STFU);
        }

        
        var request = sentStuff.SingleOrDefault(r => Guid.Parse(r.RequesterId) == userId && r.RequestedName == friendshipRequestDTO.RequestedName);
        
        if (request == null)
        {
            return Result<FriendshipRequestDTO>.Fail("Friend request not found.");
        }

        if (request.RequestedId != null)
        {
            var req = getSentRequests.SingleOrDefault(r => r.RequestingUserId == Guid.Parse(friendshipRequestDTO.RequestedId) && r.RequestedUserId == Guid.Parse(request.RequestedId));
            await _friendshipRepository.RemoveFriendRequest(req);
        }
   
        return Result<FriendshipRequestDTO>.Ok(friendshipRequestDTO);
    }

    public async Task<Result<List<FriendshipDTO>>> GetFriends(Guid userId)
    {
        var results = await _friendshipRepository.GetFriends(userId);
        var userOne = await _userService.GetUserById(userId);
        var friends = new List<FriendshipDTO>();
        
        foreach (var result in results)
        {
            var friendDetails = await _userService.GetUserById(result);
            var mapping = new FriendshipDTO
            {
                FriendOneId = userOne.Value.Id,
                FriendOneName = userOne.Value.Username,
                FriendTwoId = friendDetails.Value.Id,
                FriendTwoName = friendDetails.Value.Username,
            };
            
            friends.Add(mapping);
        }
        
        return Result<List<FriendshipDTO>>.Ok(friends);
    }

    public async Task<Result<FriendshipDTO>> RemoveFriendship(FriendshipDTO friendshipDTO)
    {
        var friendshipOneId = Guid.Parse(friendshipDTO.FriendOneId);
        var friendshipTwoId = Guid.Parse(friendshipDTO.FriendTwoId);
        var checkFriendshipExistence = await _friendshipRepository.GetFriendship(friendshipOneId, friendshipTwoId);
        if (checkFriendshipExistence == null)
        {
            return Result<FriendshipDTO>.Fail("FriendshipEntity not found.");
        }

        await _friendshipRepository.RemoveFriendship(checkFriendshipExistence);
        
        return Result<FriendshipDTO>.Ok(friendshipDTO);
    }
}