using web_api.DataAccess.Messages;
using web_api.Dtos.Messages;
using web_api.Utils;
using web_api.Data;
using web_api.Services.Users;

namespace web_api.Services.Messages;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserService _userService;
    
    public MessageService(IMessageRepository messageRepository, IUserService userService)
    {
        _messageRepository = messageRepository;
        _userService = userService;
    }
    
    public async Task<Result<List<MessageDTO>>> GetMessagesByChannelId(Guid channelId)
    {
        var messages = await _messageRepository.GetMessagesByChannelId(channelId);
        var mappedMessages = new List<MessageDTO>();
        
        foreach (var message in messages)
        {
            var user = await _userService.GetUserById(message.UserId);
            var newMessage = new MessageDTO
            {
                Id = message.Id.ToString(),
                SenderId = message.UserId.ToString(),
                SenderUsername = user.Value.Username,
                ChannelId = message.ChannelId.ToString(),
                Text = message.MessageText,
                Created = message.CreatedAt,
            };
            mappedMessages.Add(newMessage);
        }
        
        return Result<List<MessageDTO>>.Ok(mappedMessages);
    }

    public Task<Result<MessageDTO>> GetMessageById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<string>> DeleteMessageById(Guid id)
    {
        var checkMessageExistence = await _messageRepository.GetMessageById(id);
        if (checkMessageExistence == null)
        {
            return Result<string>.Fail("Message not found.");
        }
        
        await _messageRepository.DeleteMessageById(id);
        return Result<string>.Ok("Message successfully deleted.");
    }

    public async Task<Result<MessageDTO>> CreateMessage(NewMessageDTO message)
    {
        var mappedMessage = new Message
        {
            Id = Guid.NewGuid(),
            MessageText = message.Text,
            ChannelId = Guid.Parse(message.ChannelId),
            UserId = Guid.Parse(message.SenderId),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        

        var dbResults = await _messageRepository.CreateMessage(mappedMessage);
        var results = new MessageDTO
        {
            Id = dbResults.Id.ToString(),
            SenderId = dbResults.UserId.ToString(),
            ChannelId = dbResults.ChannelId.ToString(),
            Text = dbResults.MessageText,
            Created = dbResults.CreatedAt,
        };
        
        return Result<MessageDTO>.Ok(results);
    }
}