using WebChat.Shared.Common;
using WebChat.Shared.Models.Spaces;

namespace WebChat.Service.Services.Spaces;

public interface ISpacesService
{
    Task<Result<Space>> CreateSpaceAsync(SpaceToAdd space, Guid userId);
    Task<Result<List<Space>>> GetSpacesByUserIdAsync(Guid userId);
    Task<Result<Space>> GetSpaceByIdAsync(Guid id);
    Task<Result<List<UserSpaceMember>>> AddUserToSpaceAsync(Guid spaceId, Guid userId);
    Task<Result<List<UserSpaceMember>>> RemoveUserFromSpaceAsync(Guid spaceId, Guid userId);
    Task<Result<bool>> DeleteSpaceByIdAsync(Guid spaceId);
}