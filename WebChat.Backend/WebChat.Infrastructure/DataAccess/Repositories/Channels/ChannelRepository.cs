using Microsoft.EntityFrameworkCore;
using WebChat.Infrastructure.DataAccess.Mappers;
using WebChat.Shared.Models.Channels;

namespace WebChat.Infrastructure.DataAccess.Repositories.Channels;

public class ChannelRepository : IChannelRepository
{
    private readonly WebChatDbContext _context;
    
    public ChannelRepository(WebChatDbContext context)
    {
        _context = context;
    }
    
    public async Task<Channel> CreateChannelAsync(Channel channel)
    {
        var newChannel = channel.ToEntity();
        var result = await _context.Channels.AddAsync(newChannel);
        await _context.SaveChangesAsync();

        return result.Entity.ToModel();
    }

    public async Task<List<Channel>> GetChannelsBySpaceIdAsync(Guid spaceId)
    {
        var query = await _context.Channels.Where(c => c.SpaceId == spaceId).ToListAsync();
        var results = query.Select(c => c.ToModel()).ToList();
        
        return results;
    }

    public async Task<Channel?> GetChannelByIdAsync(Guid channelId)
    {
        var query = await _context.Channels.FindAsync(channelId);
        var results = query?.ToModel();
        
        return results;
    }

    public async Task<Guid> DeleteChannelByIdAsync(Guid channelId)
    {
        var channel = await _context.Channels.Where(c => c.Id == channelId).SingleOrDefaultAsync(); 
        _context.Channels.Remove(channel);
        await _context.SaveChangesAsync();
        
        return channel.Id;
    }

    public async Task<int> DeleteChannelMessagesAsync(Guid id)
    {
        var query = await _context.Channels.Where(c => c.Id == id).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();

        return query;
    }
}