using WebChat.Infrastructure.DataAccess.Repositories.RefreshTokens;
using WebChat.Service.Services.Auth.Interfaces;
using WebChat.Shared.Common;
using WebChat.Shared.Models.Auth;

namespace WebChat.Service.Services.Auth;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    
    public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }
    
    public async Task<Result<RefreshToken>> AddRefreshTokenAsync(RefreshToken refreshToken)
    {
       var result = await _refreshTokenRepository.AddRefreshTokenAsync(refreshToken);
       
       return Result<RefreshToken>.Ok(result);
    }

    public async Task<Result<RefreshToken>> GetRefreshTokenByIdAsync(Guid tokenId)
    {
        var result = await _refreshTokenRepository.GetRefreshTokenByIdAsync(tokenId);
        
        return Result<RefreshToken>.Ok(result);
    }

    public async Task<Result<RefreshToken>> GetRefreshTokenByUserIdAsync(Guid userId)
    {
        var result = await _refreshTokenRepository.GetRefreshTokenByUserIdAsync(userId);
        
        return Result<RefreshToken>.Ok(result);
    }

    public async Task<Result<bool>> RemoveRefreshTokenAsync(RefreshToken refreshToken)
    {
        throw new NotImplementedException();
    }
}