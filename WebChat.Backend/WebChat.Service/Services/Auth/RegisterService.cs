using web_api.Data;
using web_api.DataAccess.Users;
using web_api.Dtos.Auth;
using web_api.Services.Users;
using web_api.Utils;

namespace web_api.Services.Auth;

public class RegisterService : IRegisterService
{
    private readonly IUsersRepository UsersRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IUserService _userService;
    private readonly IPasswordHash _passwordHash;
    private readonly IJWTService _jwtService;
    
    public RegisterService(IUsersRepository usersRepository, IUserRoleRepository userRoleRepository, IPasswordHash passwordHash, IJWTService jwtService, IUserService userService)
    {
        UsersRepository  = usersRepository;
        _userRoleRepository = userRoleRepository;
        _userService = userService;
        _passwordHash = passwordHash;
        _jwtService = jwtService;
    }

    public async Task<Result<string>> CreateUser(UserRegisterDto newUserDto)
    {
        var newUserEmail = newUserDto.Email;
        var checkUserExistenceByUsername = await _userService.GetUserByUsername(newUserDto.Username);
        var checkUserExistenceByEmail = await _userService.GetUserByEmail(newUserDto.Email);

        if (checkUserExistenceByEmail.Success)
        {
            return Result<string>.Fail("User with email already exists.");
        }

        if (checkUserExistenceByUsername.Success)
        {
            return Result<string>.Fail("User with username already exists.");
        }
        
      
            var newUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = newUserEmail,
                UserName = newUserDto.Username,
                FirstName = newUserDto.FirstName,
                LastName = newUserDto.LastName,
                Password = _passwordHash.HashPassword(newUserDto.Password),
                EnableNotifications = false,
                PhoneNumber = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            try
            {
                await UsersRepository.CreateUser(newUser);
                var token = _jwtService.GenerateToken(newUser);
                _userRoleRepository.CreateUserRole(newUser);
                return Result<string>.Ok(token);
            }
            catch (Exception ex)
            {
                return Result<string>.Fail("An error occured while creating user.");
            }
    }
    
    public static Guid Int2Guid(int value)
    {
        byte[] bytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(bytes, 0);
        return new Guid(bytes);
    }

    public static int Guid2Int(Guid value)
    {
        byte[] b = value.ToByteArray();
        int bint = BitConverter.ToInt32(b, 0);
        return bint;
    }
}