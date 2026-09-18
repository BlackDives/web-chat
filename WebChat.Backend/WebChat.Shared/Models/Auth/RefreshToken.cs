using WebChat.Shared.Models.Users;

namespace WebChat.Shared.Models.Auth;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public bool IsActive { get; set; }
    public bool IsRevoked { get; set; }
    public DateTimeOffset ExpiresOn { get; set; }
}