using WebChat.Shared.Models.Messages;

namespace WebChat.Infrastructure.DataAccess.Repositories.Messages;

public interface IMessageRepository
{
    public Task<Message?> GetMessageByIdAsync(Guid id);
    public Task<List<Message>> GetMessagesByChannelIdAsync(Guid channelId);
    public Task<Message?> CreateMessageAsync(Message message);
    public Task<Message> DeleteMessageByIdAsync(Guid id);
}