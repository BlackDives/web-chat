

using WebChat.Shared.Common;
using WebChat.Shared.Models.Messages;

namespace WebChat.Service.Services.Messages;

public interface IMessageService
{
    public Task<Result<Message>> CreateMessageAsync(MessageToCreate newMessage);
    public Task<Result<PagedResult<Message>>> GetMessagesByChannelIdAsync(Guid channelId);
    public Task<Result<Message>> GetMessageByIdAsync(Guid id);
    public Task<Result<bool>> DeleteMessageByIdAsync(Guid id);
}