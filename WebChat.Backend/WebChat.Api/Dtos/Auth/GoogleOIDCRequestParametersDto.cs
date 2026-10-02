namespace WebChat.Api.Dtos.Auth;

public class GoogleOIDCRequestParametersDto
{
    public string State { get; set; }
    public string Iss {  get; set; }
    public string Code {  get; set; }
    public string Scope {  get; set; }
    public string AuthUser { get; set; }
    public string Prompt { get; set; }
}