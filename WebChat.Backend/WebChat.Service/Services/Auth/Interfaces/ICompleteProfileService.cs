using System.Security.Claims;
using WebChat.Shared.Common;
using WebChat.Shared.Models.Auth;

namespace WebChat.Service.Services.Auth;

public interface ICompleteProfileService
{
    Task<Result<AuthenticatedUser?>> CompleteUserCreationAsync(CompletedUserProfile completedUserProfile, string completeProfileToken);
}