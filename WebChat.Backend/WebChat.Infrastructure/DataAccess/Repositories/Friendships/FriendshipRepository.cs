using Microsoft.EntityFrameworkCore;
using WebChat.Infrastructure.DataAccess.Mappers;
using WebChat.Shared.Enums;
using WebChat.Shared.Models.Friendships;

namespace WebChat.Infrastructure.DataAccess.Repositories.Friendships;

public class FriendshipRepository : IFriendshipRepository
{
    private readonly WebChatDbContext _dbContext;
    
    public FriendshipRepository(WebChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Friendship>> ListSentFriendRequestsAsync(Guid userId)
    {
        var query = await _dbContext.Friendships.Where(f => f.SenderId == userId && f.FriendshipStatus == FriendshipStatusEnum.Pending)
            .ToListAsync();
        var results = query.Select(f => f.ToModel()).ToList();
        
        return results;
    }

    public async Task<List<Friendship>> ListReceivedFriendRequestsAsync(Guid userId)
    {
        var query = await  _dbContext.Friendships.Where(f => f.ReceiverId == userId && f.FriendshipStatus == FriendshipStatusEnum.Pending)
            .ToListAsync();
        var results = query.Select(f => f.ToModel()).ToList();
        
        return results;
    }

    public async Task<Friendship?> GetFriendshipAsync(Guid userOneId, Guid userTwoId)
    {
        var friendship = await _dbContext.Friendships.FirstOrDefaultAsync(f => (f.SenderId == userOneId && f.ReceiverId == userTwoId) 
        || (f.SenderId == userTwoId && f.ReceiverId == userOneId));
        
        var results = friendship?.ToModel();
        
        return results;
    }

    public async Task<List<Friendship>> GetFriendsByIdAsync(Guid userId)
    {
        var query = await _dbContext.Friendships
            .Where(f => (f.SenderId == userId || f.ReceiverId == userId) && f.FriendshipStatus == FriendshipStatusEnum.Active)
            .ToListAsync();
        var results = query.Select(f => f.ToModel()).ToList();
        
        return results;
    }

    public async Task<Friendship> CreateFriendshipAsync(Friendship friendship)
    {
        var newFriendship = friendship.ToEntity();
        await _dbContext.Friendships.AddAsync(newFriendship);
        await _dbContext.SaveChangesAsync();
        
        return friendship;
    }

    public async Task<Friendship> UpdateFriendshipStatusAsync(Friendship friendship)
    {
        var mappedFriendship = friendship.ToEntity();
        
        var query = await _dbContext.Friends
            .Where(f => f.Id == friendship.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(f => f.FriendshipStatus, mappedFriendship.FriendshipStatus)
                .SetProperty(f => f.ReceivedOn, mappedFriendship.ReceivedOn)
                .SetProperty(f => f.FriendsOn, mappedFriendship.FriendsOn)
                .SetProperty(f => f.DeclinedOn, mappedFriendship.DeclinedOn)
                .SetProperty(f => f.BlockedOn, mappedFriendship.BlockedOn));
        await _dbContext.SaveChangesAsync();
        
        return friendship;
    }
}