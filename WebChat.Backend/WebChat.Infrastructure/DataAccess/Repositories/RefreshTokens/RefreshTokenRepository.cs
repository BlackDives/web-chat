using Microsoft.EntityFrameworkCore;
using WebChat.Shared.Models.Auth;
using WebChat.Infrastructure.DataAccess.Mappers;

namespace WebChat.Infrastructure.DataAccess.Repositories.RefreshTokens;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly WebChatDbContext _dbContext;
    
    public RefreshTokenRepository(WebChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<RefreshToken> GetRefreshTokenByIdAsync(Guid id)
    {
        var refreshToken = await _dbContext.RefreshTokens.Where(rt => rt.Id == id).FirstAsync();
        var results = refreshToken.ToModel();

        return results;
    }

    public async Task<RefreshToken> GetRefreshTokenByUserIdAsync(Guid id)
    {
        var refreshToken = await _dbContext.RefreshTokens.Where(rt => rt.UserId == id).FirstAsync();
        var results = refreshToken.ToModel();
        
        return results;
    }

    public async Task<RefreshToken> AddRefreshTokenAsync(RefreshToken refreshToken)
    {
        var mappedToken = refreshToken.ToEntity();
        await _dbContext.RefreshTokens.AddAsync(mappedToken);
        await _dbContext.SaveChangesAsync();
        
        return refreshToken;
    }

    public async Task<Guid> DeleteRefreshTokenByIdAsync(Guid id)
    {
        var query = await _dbContext.RefreshTokens.Where(rt => rt.Id == id).ExecuteDeleteAsync();
        await _dbContext.SaveChangesAsync();

        return id;
    }
}