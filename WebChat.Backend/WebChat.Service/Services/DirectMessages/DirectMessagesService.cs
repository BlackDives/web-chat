using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using web_api.Data;
using web_api.DataAccess.DirectMessages;
using web_api.Dtos.DirectMessages;
using web_api.Services.Users;
using web_api.Utils;

namespace web_api.Services.DirectMessages;

public class DirectMessagesService : IDirectMessageService
{
    private readonly IDirectMessagesRepository _directMessageRepository;
    private readonly IUserService _userService;

    public DirectMessagesService(IDirectMessagesRepository directMessageRepository, IUserService userService)
    {
        _directMessageRepository = directMessageRepository;
        _userService = userService;
    }
    
    public async Task<Result<List<DirectMessageChannelDTO>>> GetDirectMessageChannelsByUserId(Guid userId)
    {
        var directMessageChannels = await _directMessageRepository.GetDirectMessageChannelsByUserId(userId);
        var user = await _userService.GetUserById(userId);
        var cleanDMCChannelsList = new List<DirectMessageChannel>();
        var mappedDirectMessageChannels = new List<DirectMessageChannelDTO>();

        foreach (var directMessageChannel in directMessageChannels)
        {
            if (directMessageChannel.UserOneId != userId)
            {
                var newDMC = new DirectMessageChannel
                {
                    DirectMessageChannelId = directMessageChannel.DirectMessageChannelId,
                    UserOneId = userId,
                    UserTwoId = directMessageChannel.UserOneId,
                    CreatedAt = directMessageChannel.CreatedAt,
                    Messages = directMessageChannel.Messages
                };
                
                cleanDMCChannelsList.Add(newDMC);
            }
            else
            {
                var newDMC = new DirectMessageChannel
                {
                    DirectMessageChannelId = directMessageChannel.DirectMessageChannelId,
                    UserOneId = userId,
                    UserTwoId = directMessageChannel.UserTwoId,
                    CreatedAt = directMessageChannel.CreatedAt,
                    Messages = directMessageChannel.Messages
                };
                
                cleanDMCChannelsList.Add(newDMC);

            }
        }
        

        foreach (var cleanDMC in cleanDMCChannelsList)
        {
            var otherUser = await _userService.GetUserById(cleanDMC.UserTwoId);

            var mappedDMC = new DirectMessageChannelDTO
            {
                Id = cleanDMC.DirectMessageChannelId.ToString(),
                UserOneId = userId.ToString(),
                UserOneUsername = user.Value.Username,
                UserTwoUsername = otherUser.Value.Username,
                CreatedAt = cleanDMC.CreatedAt,
                UpdatedAt = cleanDMC.UpdatedAt
            };
            
            mappedDirectMessageChannels.Add(mappedDMC);
        }

        return Result<List<DirectMessageChannelDTO>>.Ok(mappedDirectMessageChannels);
    }

    public async Task<Result<DirectMessageChannelDTO>> DeleteDirectMessageChannel(Guid directMessageChannelId)
    {
        var checkChannelExists = await _directMessageRepository.GetDirectMessageChannel(directMessageChannelId);
        if (checkChannelExists != null)
        {
            var userOne = await _userService.GetUserById(checkChannelExists.UserOneId);
            var userTwo = await _userService.GetUserById(checkChannelExists.UserTwoId);
            
            var dmcMessages = await _directMessageRepository.GetDirectMessagesByChannelId(directMessageChannelId);
            foreach (var dmcMessage in dmcMessages)
            {
                await _directMessageRepository.DeleteDirectMessage(dmcMessage.MessageId);
            }
            await _directMessageRepository.DeleteDirectMessageChannel(directMessageChannelId);

            var mappedChannel = new DirectMessageChannelDTO
            {
                Id = directMessageChannelId.ToString(),
                UserOneId = userOne.Value.Id,
                UserOneUsername = userOne.Value.Username,
                UserTwoId = userTwo.Value.Id,
                UserTwoUsername = userTwo.Value.Username,
                CreatedAt = checkChannelExists.CreatedAt,
                UpdatedAt = checkChannelExists.UpdatedAt
            };
            return Result<DirectMessageChannelDTO>.Ok(mappedChannel);
        }
        
        return Result<DirectMessageChannelDTO>.Fail("ChannelEntity not found.");
    }

    public async Task<Result<DirectMessageDTO>> DeleteDirectMessage(Guid directMessageId)
    {
        var checkDMMessageExists = await _directMessageRepository.GetDirectMessageById(directMessageId);
        if (checkDMMessageExists != null)
        {
            var results  = await _directMessageRepository.DeleteDirectMessage(directMessageId);
            var user = await _userService.GetUserById(checkDMMessageExists.SenderId);

            var mappedMessage = new DirectMessageDTO
            {
                MessageId = checkDMMessageExists.MessageId.ToString(),
                DirectMessageChannelId = checkDMMessageExists.DirectMessageChannelId.ToString(),
                SenderId = checkDMMessageExists.SenderId.ToString(),
                SenderUsername = user.Value.Username,
                CreatedAt = checkDMMessageExists.CreatedAt,
                UpdatedAt = checkDMMessageExists.UpdatedAt
            };
            
            return Result<DirectMessageDTO>.Ok(mappedMessage);
        }
        
        return Result<DirectMessageDTO>.Fail("Message not found.");
    }

    public async Task<Result<DirectMessageChannelDTO>> GetDirectMessageChannelById(Guid directMessageChannelId)
    {
        var directMessageChannel = await _directMessageRepository.GetDirectMessageChannel(directMessageChannelId);
        if (directMessageChannel == null)
        {
            return Result<DirectMessageChannelDTO>.Fail("Direct message channelEntity not found.");
        }
        
        var userOne = await _userService.GetUserById(directMessageChannel.UserOneId);
        var userTwo = await _userService.GetUserById(directMessageChannel.UserTwoId);

        var mappedDirectMessageChannel = new DirectMessageChannelDTO
        {
            Id = directMessageChannel.DirectMessageChannelId.ToString(),
            UserOneId = userOne.Value.Id.ToString(),
            UserOneUsername = userOne.Value.Username,
            UserTwoId = userTwo.Value.Id.ToString(),
            UserTwoUsername = userTwo.Value.Username,
            CreatedAt = directMessageChannel.CreatedAt,
            UpdatedAt = directMessageChannel.UpdatedAt
        };
        
        return Result<DirectMessageChannelDTO>.Ok(mappedDirectMessageChannel);
    }

    public async Task<Result<DirectMessageChannelDTO>> CreateDirectMessageChannel(DirectMessageChannelDTO directMessageChannelDto)
    {
        var userOneId = Guid.Parse(directMessageChannelDto.UserOneId);
        var userTwoId = Guid.Parse(directMessageChannelDto.UserTwoId);
        var checkExistence = await _directMessageRepository.GetDirectMessageChannelByUserIds(userOneId, userTwoId);

        if (checkExistence != null)
        {
            return Result<DirectMessageChannelDTO>.Fail("DM already exists.");
        }

        var directMessageChannel = new DirectMessageChannel
        {
            DirectMessageChannelId = Guid.NewGuid(),
            UserOneId = userOneId,
            UserTwoId = userTwoId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        
        var results = await _directMessageRepository.CreateDirectMessageChannel(directMessageChannel);
        directMessageChannelDto.Id = results.DirectMessageChannelId.ToString();
        directMessageChannelDto.CreatedAt = results.CreatedAt;
        directMessageChannelDto.UpdatedAt = results.UpdatedAt;
        
        return Result<DirectMessageChannelDTO>.Ok(directMessageChannelDto);
    }

    public async Task<Result<DirectMessageChannelDTO>> GetDirectMessageChannelByUserIds(Guid userOneId, Guid userTwoId)
    {
        var directMessageChannel = await _directMessageRepository.GetDirectMessageChannelByUserIds(userOneId, userTwoId);
        if (directMessageChannel == null)
        {
            return Result<DirectMessageChannelDTO>.Fail("DM doesn't exist.");
        }
        
        var userOne = await _userService.GetUserById(directMessageChannel.UserOneId);
        var userTwo = await _userService.GetUserById(directMessageChannel.UserTwoId);

        var mappedDirectMessageChannel = new DirectMessageChannelDTO
        {
            Id = directMessageChannel.DirectMessageChannelId.ToString(),
            UserOneId = userOne.Value.Id.ToString(),
            UserOneUsername = userOne.Value.Username,
            UserTwoId = userTwo.Value.Id.ToString(),
            UserTwoUsername = userTwo.Value.Username,
            CreatedAt = directMessageChannel.CreatedAt,
            UpdatedAt = directMessageChannel.UpdatedAt
        };
        
        return Result<DirectMessageChannelDTO>.Ok(mappedDirectMessageChannel);
    }

    public async Task<Result<List<DirectMessageDTO>>> GetDirectMessagesByChannelId(Guid channelId)
    {
        var messages = await _directMessageRepository.GetDirectMessagesByChannelId(channelId);
        var mappedDirectMessages = new List<DirectMessageDTO>();
        
        foreach (var message in messages)
        { 
            var sender = await _userService.GetUserById(message.SenderId);
            var newMappedMessage = new DirectMessageDTO
            {
                MessageId = message.MessageId.ToString(),
                DirectMessageChannelId = message.DirectMessageChannelId.ToString(),
                SenderId = message.SenderId.ToString(),
                SenderUsername = sender.Value.Username,
                MessageContent = message.MessageText,
                CreatedAt = message.CreatedAt,
                UpdatedAt = message.UpdatedAt
            };
            
            mappedDirectMessages.Add(newMappedMessage);
        }
        
        return Result<List<DirectMessageDTO>>.Ok(mappedDirectMessages);
    }

    public async Task<Result<DirectMessageDTO>> GetDirectMessageById(Guid directMessageChannelId)
    {
        var directMessage = await _directMessageRepository.GetDirectMessageById(directMessageChannelId);
        if (directMessage != null)
        {
            var sender = await _userService.GetUserById(directMessage.SenderId);
            var mappedMessage = new DirectMessageDTO
            {
                MessageId = directMessage.MessageId.ToString(),
                DirectMessageChannelId = directMessage.DirectMessageChannelId.ToString(),
                SenderId = directMessage.SenderId.ToString(),
                SenderUsername = sender.Value.Username,
                CreatedAt = directMessage.CreatedAt,
                UpdatedAt = directMessage.UpdatedAt
            };
            
            return Result<DirectMessageDTO>.Ok(mappedMessage);
        }
        
        return Result<DirectMessageDTO>.Fail("Message doesn't exist.");
    }

    public async Task<Result<DirectMessageDTO>> CreateDirectMessage(DirectMessageDTO directMessageDto)
    {
        var newMessage = new DirectMessage
        {
            MessageId = Guid.NewGuid(),
            DirectMessageChannelId = Guid.Parse(directMessageDto.DirectMessageChannelId),
            SenderId = Guid.Parse(directMessageDto.SenderId),
            MessageText = directMessageDto.MessageContent,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var results = await _directMessageRepository.CreateDirectMessages(newMessage);
        
        directMessageDto.MessageId = results.MessageId.ToString();
        directMessageDto.CreatedAt = results.CreatedAt;
        directMessageDto.UpdatedAt = results.UpdatedAt;
        
        return Result<DirectMessageDTO>.Ok(directMessageDto);

    }
}