

using WebChat.Infrastructure.DataAccess.Repositories.Messages;
using WebChat.Service.Services.Users;
using WebChat.Shared.Common;
using WebChat.Shared.Models.Messages;

namespace WebChat.Service.Services.Messages;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserService _userService;
    
    public MessageService(IMessageRepository messageRepository, IUserService userService)
    {
        _messageRepository = messageRepository;
        _userService = userService;
    }


    public async Task<Result<Message>> CreateMessageAsync(MessageToCreate newMessage)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PagedResult<Message>>> GetMessagesByChannelIdAsync(Guid channelId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Message>> GetMessageByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> DeleteMessageByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}