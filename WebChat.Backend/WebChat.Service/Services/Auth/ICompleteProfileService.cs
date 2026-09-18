using System.Security.Claims;
using WebChat.Shared.Models.Auth;

namespace WebChat.Service.Services.Auth;

public interface ICompleteProfileService
{
    CompleteProfileUser GetCompleteProfileToken(ClaimsPrincipal claimsPrincipal);
}