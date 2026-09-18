using web_api.Dtos.Messages;
using web_api.Utils;

namespace web_api.Services.Messages;

public interface IMessageService
{
    public Task<Result<List<MessageDTO>>> GetMessagesByChannelId(Guid channelId);
    public Task<Result<MessageDTO>> GetMessageById(Guid id);
    public Task<Result<string>> DeleteMessageById(Guid id);
    public Task<Result<MessageDTO>> CreateMessage(NewMessageDTO
        message);
}