using System.Security.Claims;
using WebChat.Shared.Common;
using WebChat.Shared.Models.Auth;

namespace WebChat.Service.Services.Auth;

public interface IGoogleAuthService
{
    Task<Result<AuthenticatedUser>> GoogleSignInAsync(ClaimsPrincipal userClaims);
    
    /// <summary>
    /// Tries to retrieve a user based on their Google OIDC token.
    /// </summary>
    /// <param name="googleIdToken">The Google OIDC token for the authenticating user.</param>
    /// <returns><see cref="AuthenticatedUser"/>></returns>
    Task<Result<AuthenticatedUser?>> GoogleSignInAsync(string googleIdToken);
    
    /// <summary>
    /// Returns a complete profile token for the new user
    /// </summary>
    /// <param name="googleIdToken"></param>
    /// <returns></returns>
    Task<Result<CompleteProfileUser>> GoogleSignUpAsync(string googleIdToken);
}