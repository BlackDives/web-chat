using WebChat.Service.Services.Channels;
using WebChat.Infrastructure.DataAccess.Repositories.Spaces;
using WebChat.Service.Services.Users;
using WebChat.Shared.Common;
using WebChat.Shared.Models.Spaces;

namespace WebChat.Service.Services.Spaces;
public class SpacesService : ISpacesService
{
    private readonly ISpacesRepository _spacesRepository;
    private readonly IUserService _userService;
    private readonly IChannelService _channelService;

    public SpacesService(ISpacesRepository spacesRepository, IUserService usersService, IChannelService channelService)
    {
        _spacesRepository = spacesRepository;
        _userService = usersService;
        _channelService = channelService;
    }

    public async Task<Result<Space>> CreateSpaceAsync(SpaceToAdd space, Guid userId)
    {
        var now = DateTimeOffset.UtcNow;
        var newSpace = new Space
        {
            Id = Guid.NewGuid(),
            Name = space.Name,
            ServerIcon = space.ServerIcon,
            CreatedAt = now,
            UpdatedAt = now,
        };

        var results = await _spacesRepository.CreateSpaceAsync(newSpace);
        
        return Result<Space>.Ok(results);
    }

    public async Task<Result<List<Space>>> GetSpacesByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Space>> GetSpaceByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<UserSpaceMember>>> AddUserToSpaceAsync(Guid spaceId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<UserSpaceMember>>> RemoveUserFromSpaceAsync(Guid spaceId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> DeleteSpaceByIdAsync(Guid spaceId)
    {
        throw new NotImplementedException();
    }
}