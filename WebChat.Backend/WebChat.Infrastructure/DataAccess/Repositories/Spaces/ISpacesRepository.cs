using WebChat.Shared.Common;
using WebChat.Shared.Models.Spaces;
using WebChat.Shared.Models.Users;

namespace WebChat.Infrastructure.DataAccess.Repositories.Spaces;

public interface ISpacesRepository
{
    Task<Space> CreateSpaceAsync(Space space);
    
    Task<List<Space>> ListSpacesAsync();
    
    Task<List<User>> ListSpaceMembersAsync(Guid id);
    
    Task<Space> GetSpaceByIdAsync(Guid id);
    
    Task<List<Space>> ListOwnedSpacesByIdAsync(Guid userId);
    
    Task<List<Space>> ListSpaceMembershipsByIdAsync(Guid userId);
    
    Task<UserSpaceMember> AddNewSpaceMemberAsync(UserSpaceMember userSpaceMember);
    
    Task<Guid> RemoveUserFromSpaceByUserIdAsync(Guid spaceId, Guid userId);
    
    Task<Guid> DeleteSpaceByIdAsync(Guid id);
    
    Task<int> RemoveAllSpaceMembersAsync(Guid id);
}