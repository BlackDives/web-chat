using WebChat.Shared.Models.Friendships;

namespace WebChat.Infrastructure.DataAccess.Repositories.Friendships;

public interface IFriendshipRepository
{
    Task<List<Friendship>> ListSentFriendRequestsAsync(Guid userId);
    
    Task<List<Friendship>> ListReceivedFriendRequestsAsync(Guid userId);
    
    /// <summary>
    /// Returns record matching the combination of user IDs.
    /// </summary>
    /// <param name="userOneId">ID of the first user.</param>
    /// <param name="userTwoId">ID of the second user.</param>
    /// <returns><see cref="Friendship"/> if existing, null otherwise.</returns>
    Task<Friendship?> GetFriendshipAsync(Guid userOneId, Guid userTwoId);
    
    Task<List<Friendship>> GetFriendsByIdAsync(Guid userId);
    
    Task<Friendship> CreateFriendshipAsync(Friendship friendship); 
    
    Task<Friendship> UpdateFriendshipStatusAsync(Friendship friendship);
}