using web_api.Data;
using web_api.Dtos;
using web_api.Utils;

namespace web_api.Services.Channels;

public interface IChannelService
{
    Task<List<ChannelDTO>> GetChannelsByServerId(Guid serverId);
    Task<Result<ChannelDTO>> GetChannelById(Guid channelId);
    Task<Channel> AddServerChannel(Guid serverId, ChannelDTO channel);

    Task<Result<string>> RemoveChannelMessagesByChannelId(Guid channelId);
    
    Task<Result<string>> DeleteChannelById(Guid serverId);
}