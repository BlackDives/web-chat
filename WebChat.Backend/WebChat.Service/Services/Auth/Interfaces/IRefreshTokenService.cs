using WebChat.Shared.Common;
using WebChat.Shared.Models.Auth;

namespace WebChat.Service.Services.Auth.Interfaces;

public interface IRefreshTokenService
{
    Task<Result<RefreshToken>> AddRefreshTokenAsync(RefreshToken refreshToken);
    
    Task<Result<RefreshToken>> GetRefreshTokenByIdAsync(Guid tokenId);
    
    Task<Result<RefreshToken>> GetRefreshTokenByUserIdAsync(Guid userId);
    
    Task<Result<bool>> RemoveRefreshTokenAsync(RefreshToken refreshToken);
}