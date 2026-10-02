using Microsoft.EntityFrameworkCore;
using WebChat.Infrastructure.DataAccess.Mappers;
using WebChat.Shared.Models.Messages;

namespace WebChat.Infrastructure.DataAccess.Repositories.Messages;

public class MessageRepository : IMessageRepository
{
    private readonly WebChatDbContext _dbContext;
    
    public MessageRepository(WebChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Message?> GetMessageByIdAsync(Guid id)
    {
        var message = await _dbContext.Messages.Where(m => m.Id == id).SingleOrDefaultAsync();
        if (message == null)
        {
            return null;
        }

        return message.MapToModel();
    }

    public async Task<List<Message>> GetMessagesByChannelIdAsync(Guid channelId)
    {
        var messages = await _dbContext.Messages.Where(m => m.ChannelId == channelId).ToListAsync();
        var results = messages.Select(m => m.MapToModel()).ToList();

        return results;
    }

    public async Task<Message?> CreateMessageAsync(Message message)
    {
       var query = await _dbContext.Messages.AddAsync(message.MapToEntity());
       await _dbContext.SaveChangesAsync();
       var results = await _dbContext.Messages.FirstOrDefaultAsync(m => m.Id == message.Id);

       if (results == null)
       {
           return null;
       }
       
       return results.MapToModel();
    }

    public async Task<Message> DeleteMessageByIdAsync(Guid id)
    {
        var results = await _dbContext.Messages.Where(m => m.Id == id).SingleAsync();
        _dbContext.Messages.Remove(results);
        await _dbContext.SaveChangesAsync();
        
        return results.MapToModel();
    }
}