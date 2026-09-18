using web_api.Data;
using web_api.DataAccess;
using web_api.Dtos;
using web_api.ServiceMessages.Channel;
using web_api.Services.Messages;
using web_api.Utils;

namespace web_api.Services.Channels;

public class ChannelService : IChannelService
{
    private readonly IChannelRepository _channelRepository;
    private readonly IMessageService _messageService;
    
    public ChannelService(IChannelRepository channelRepository, IMessageService messageService)
    {
        _channelRepository = channelRepository;
        _messageService = messageService;
    }
    public async Task<Channel> AddServerChannel(Guid serverId, ChannelDTO channel)
    {
        int type = 0;
        if (channel.Type.Equals("Text"))
        {
            type = 1;
        }
        else if (channel.Type.Equals("Voice"))
        {
            type = 2;
        }

        var newChannel = new Channel
        {
            Id = Guid.NewGuid(),
            ServerId = serverId,
            Name = channel.Name,
            Type = type,
        };

        var result = await _channelRepository.Create(newChannel);
        return result;
    }

    public async Task<Result<ChannelDTO>> GetChannelById(Guid channelId)
    {
        var channel = await  _channelRepository.GetChannelById(channelId);
        if (channel == null)
        {
            return Result<ChannelDTO>.Fail(ChannelServiceErrorMessage.ChannelNotFound.Value);
        }

        var result = new ChannelDTO
        {
            Id = channel.Id.ToString(),
            Name = channel.Name,
            Type = channel.Type == 1 ? "Text" : "Voice",
        };
        
        return Result<ChannelDTO>.Ok(result);
    }

    public async Task<Result<string>> RemoveChannelMessagesByChannelId(Guid channelId)
    {
        var checkChannelExistence = await _channelRepository.GetChannelById(channelId);
        if (checkChannelExistence == null)
        {
            return Result<string>.Fail("ChannelEntity not found.");
        }
        
        var channelMessages = await _messageService.GetMessagesByChannelId(channelId);
        foreach (var channelMessage in channelMessages.Value)
        {
            await _messageService.DeleteMessageById(Guid.Parse(channelMessage.Id));
        }
        
        return Result<string>.Ok("Messages removed.");
    }

    public async Task<Result<string>> DeleteChannelById(Guid serverId)
    {
        var checkChannelExistence = await _channelRepository.GetChannelById(serverId);
        if (checkChannelExistence == null)
        {
            return Result<string>.Fail("ChannelEntity not found.");
        }
        
        await _channelRepository.DeleteChannelById(serverId);
        
        return Result<string>.Ok("ChannelEntity deleted.");
    }

    public async Task<List<ChannelDTO>> GetChannelsByServerId(Guid serverId)
    {
        var channels = await _channelRepository.GetChannelsByServerId(serverId);
        
        var channelsMap = new List<ChannelDTO>();
        channels.ForEach(x =>
        {
            var temp = new ChannelDTO
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Type = x.Type == 1 ? "Text" : "Voice"
            };
            
            channelsMap.Add(temp);
        });
        
        return channelsMap;
    }
}