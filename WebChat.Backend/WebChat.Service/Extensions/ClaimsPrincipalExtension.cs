using System.Security.Claims;

namespace WebChat.Service.Extensions;

internal static class ClaimsPrincipalExtension
{
    public static Claim? GetClaim(this ClaimsPrincipal principal, string claimType)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == claimType);
    }
}