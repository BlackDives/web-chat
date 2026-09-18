
using Microsoft.EntityFrameworkCore;
using WebChat.Shared.Models.Spaces;
using WebChat.Shared.Models.Users;
using WebChat.Infrastructure.DataAccess.Mappers;

namespace WebChat.Infrastructure.DataAccess.Repositories.Spaces;

public class SpacesRepository : ISpacesRepository
{
    private readonly WebChatDbContext _dbContext;
    
    public SpacesRepository(WebChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Space> CreateSpaceAsync(Space space)
    {
        var mappedSpace = space.ToEntity(); 
        await _dbContext.AddAsync(mappedSpace);
        await _dbContext.SaveChangesAsync();
        
        return space;
    }

    public async Task<List<Space>> ListSpacesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> ListSpaceMembersAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Space> GetSpaceByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Space>> ListOwnedSpacesByIdAsync(Guid userId)
    {
        var query = await _dbContext.Spaces.Where(s => s.SpaceOwnerId == userId).ToListAsync();
        var results = query.Select(s => s.ToModel()).ToList();
        
        return results;
    }

    public async Task<List<Space>> ListSpaceMembershipsByIdAsync(Guid userId)
    {
        var query = await _dbContext.UserSpaceMembers.Where(s => s.UserId == userId).ToListAsync();
        var spaces = query.Select(usm => usm.Space).ToList();
        var results = spaces.Select(s => s.ToModel()).ToList();
        
        return results;
    }

    public async Task<UserSpaceMember> AddNewSpaceMemberAsync(UserSpaceMember userSpaceMember)
    {
        var mappedEntity = userSpaceMember.ToEntity();
        var query = await _dbContext.UserSpaceMembers.AddAsync(mappedEntity);
        await _dbContext.SaveChangesAsync();
        
        return userSpaceMember;
    }

    public async Task<Guid> RemoveUserFromSpaceByUserIdAsync(Guid spaceId, Guid userId)
    {
        var query = await _dbContext.UserSpaceMembers.Where(usm => usm.UserId == userId && usm.SpaceId == spaceId)
            .ExecuteDeleteAsync();
        await _dbContext.SaveChangesAsync();
        
        return userId;
    }

    public async Task<Guid> DeleteSpaceByIdAsync(Guid id)
    {
        var space = await _dbContext.UserSpaceMembers.Where(usm => usm.SpaceId == id).ExecuteDeleteAsync();
        await _dbContext.SaveChangesAsync();
        
        return id;
    }

    public async Task<int> RemoveAllSpaceMembersAsync(Guid id)
    {
        var query = await _dbContext.UserSpaceMembers.Where(usm => usm.SpaceId == id).ExecuteDeleteAsync();
        await _dbContext.SaveChangesAsync();
        
        return query;
    }
}