using web_api.Dtos.DirectMessages;
using web_api.Utils;

namespace web_api.Services.DirectMessages;

public interface IDirectMessageService
{
    Task<Result<List<DirectMessageChannelDTO>>> GetDirectMessageChannelsByUserId(Guid userId);
    Task<Result<DirectMessageChannelDTO>> GetDirectMessageChannelById(Guid directMessageChannelId);
    Task<Result<DirectMessageChannelDTO>> GetDirectMessageChannelByUserIds(Guid userOneId, Guid userTwoId);
    Task<Result<DirectMessageChannelDTO>> CreateDirectMessageChannel(DirectMessageChannelDTO directMessageChannelDto);
    Task<Result<DirectMessageChannelDTO>> DeleteDirectMessageChannel(Guid directMessageChannelId);
    Task<Result<List<DirectMessageDTO>> > GetDirectMessagesByChannelId(Guid channelId);
    Task<Result<DirectMessageDTO>> GetDirectMessageById(Guid directMessageChannelId);
    Task<Result<DirectMessageDTO>> CreateDirectMessage(DirectMessageDTO directMessageDto);
    Task<Result<DirectMessageDTO>> DeleteDirectMessage(Guid directMessageId);
}