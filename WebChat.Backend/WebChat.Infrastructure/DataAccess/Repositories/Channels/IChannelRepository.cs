using WebChat.Shared.Models.Channels;

namespace WebChat.Infrastructure.DataAccess.Repositories.Channels;

public interface IChannelRepository
{
    Task<Channel> CreateChannelAsync(Channel channel);
    Task<List<Channel>> GetChannelsBySpaceIdAsync(Guid spaceId);
    Task<Channel?> GetChannelByIdAsync(Guid channelId);
    
    Task<Guid> DeleteChannelByIdAsync(Guid channelId);
    
    Task<int> DeleteChannelMessagesAsync(Guid id);
}