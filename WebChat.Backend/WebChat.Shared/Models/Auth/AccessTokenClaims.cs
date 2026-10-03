namespace WebChat.Shared.Models.Auth;

public class AccessTokenClaims
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}