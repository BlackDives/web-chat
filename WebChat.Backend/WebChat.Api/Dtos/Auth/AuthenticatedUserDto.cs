namespace WebChat.Api.Dtos.Auth;

public class AuthenticatedUserDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}