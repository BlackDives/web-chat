using web_api.DataAccess.Users;
using web_api.Utils;
using web_api.Services.Users;

namespace web_api.Services.Auth;

public class LoginService : ILoginService
{
    private readonly IUserService _userService;
    private readonly IJWTService _jwtService;
    private readonly IPasswordHash _passwordHash;
    private readonly IUsersRepository _usersRepository;
    
    public LoginService(IUserService userService, IJWTService jwtService, IPasswordHash passwordHash, IUsersRepository usersRepository)
    {
        _userService = userService;
        _jwtService = jwtService;
        _passwordHash = passwordHash;
        _usersRepository = usersRepository;
    }
    public async Task<Result<string>> Login(string email, string password)
    {
        var user = await _userService.GetUserByEmail(email);
        
        if (!user.Success)
        {
            return Result<string>.Fail("User not found.");
        }

        var saledPassword = await _userService.GetUserSaltedPassword(user.Value);
        var verifyPassword = _passwordHash.VerifyPassword(saledPassword.Value, password);

        if (verifyPassword == false)
        {
            return Result<string>.Fail("Invalid password.");
        }
        
        var fullUser = await _usersRepository.GetUserByEmail(email);
        
        var token = _jwtService.GenerateToken(fullUser);
        return Result<string>.Ok(token);
    }
}