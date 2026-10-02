using WebChat.Shared.Models.Auth;
using WebChat.Shared.Models.Users;

namespace WebChat.Infrastructure.DataAccess.Repositories.RefreshTokens;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> GetRefreshTokenByIdAsync(Guid refreshTokenId);
    Task<RefreshToken> GetRefreshTokenByUserIdAsync(Guid id);
    Task<RefreshToken> AddRefreshTokenAsync(RefreshToken refreshToken);
    Task<Guid> DeleteRefreshTokenByIdAsync(Guid id);
}