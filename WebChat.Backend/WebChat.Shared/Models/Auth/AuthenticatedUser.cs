namespace WebChat.Shared.Models.Auth;

public class AuthenticatedUser
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}