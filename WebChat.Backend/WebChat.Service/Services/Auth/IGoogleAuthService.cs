using System.Security.Claims;
using WebChat.Shared.Common;
using WebChat.Shared.Models.Auth;

namespace WebChat.Service.Services.Auth;

public interface IGoogleAuthService
{
    Task<Result<AuthenticatedUser>> GoogleSignInAsync(ClaimsPrincipal userClaims);
}