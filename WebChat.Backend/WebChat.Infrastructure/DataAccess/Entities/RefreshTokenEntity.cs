namespace WebChat.Infrastructure.DataAccess.Entities;

public class RefreshTokenEntity
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    public bool IsActive { get; set; }
    public bool IsRevoked { get; set; }
    public DateTimeOffset ExpiresOn { get; set; }
}