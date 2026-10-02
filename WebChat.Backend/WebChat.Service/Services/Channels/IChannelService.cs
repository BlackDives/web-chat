using WebChat.Shared.Common;
using WebChat.Shared.Models.Channels;

namespace WebChat.Service.Services.Channels;

public interface IChannelService
{
    Task<Result<Channel>> CreateChannelAsync(ChannelToCreate channelToCreate);
    Task<Result<Channel>> GetChannelByIdAsync(Guid channelId);
    Task<Result<List<Channel>>> GetChannelsByServerIdAsync(Guid serverId);
    Task<Result<bool>> DeleteChannelByIdAsync(Guid channelId);
}