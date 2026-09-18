namespace web_api.Services;

public interface IPasswordHash
{
    string HashPassword(string password);
    bool VerifyPassword(string passwordHash, string password);
}