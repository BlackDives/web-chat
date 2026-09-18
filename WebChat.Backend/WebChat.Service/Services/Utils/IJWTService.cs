using web_api.Data;

namespace web_api.Services;

public interface IJWTService
{
    string GenerateToken(ApplicationUser user);
}