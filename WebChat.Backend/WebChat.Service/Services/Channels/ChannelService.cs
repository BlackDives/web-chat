using WebChat.Infrastructure.DataAccess.Repositories.Channels;
using WebChat.Service.Services.Messages;
using WebChat.Shared.Common;
using WebChat.Shared.Models.Channels;

namespace WebChat.Service.Services.Channels;

public class ChannelService : IChannelService
{
    private readonly IChannelRepository _channelRepository;
    private readonly IMessageService _messageService;
    
    public ChannelService(IChannelRepository channelRepository, IMessageService messageService)
    {
        _channelRepository = channelRepository;
        _messageService = messageService;
    }

    public async Task<Result<Channel>> CreateChannelAsync(ChannelToCreate channelToCreate)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Channel>> GetChannelByIdAsync(Guid channelId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<Channel>>> GetChannelsByServerIdAsync(Guid serverId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> DeleteChannelByIdAsync(Guid channelId)
    {
        throw new NotImplementedException();
    }
}