using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebChat.Api.Dtos.Auth;
using WebChat.Api.Policies;
using WebChat.Service.Services.Auth;
using WebChat.Api.Extensions.Mappers;
using WebChat.Service.Services.Auth.Interfaces;
using WebChat.Service.Services.Users;
using Webchat.Service.Services.Utils.Tokens;

namespace WebChat.Api.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IGoogleAuthService _googleAuthService;
    private readonly ICompleteProfileService _completeProfileService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly IJwtService  _jwtService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IUserService _userService;
    
    public AuthenticationController(
        IGoogleAuthService googleAuthService, 
        ICompleteProfileService completeProfileService, 
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        IJwtService jwtService,
        IRefreshTokenService refreshTokenService,
        IUserService userService
        )
    {
        _googleAuthService = googleAuthService;
        _completeProfileService = completeProfileService;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _jwtService = jwtService;
        _refreshTokenService = refreshTokenService;
        _userService = userService;
    }
    
    [HttpGet("google/login")]
    public async Task<IActionResult> GoogleSignIn([FromQuery] GoogleOIDCRequestParametersDto googleOIDCRequestParameters)
    {
        var httpClient = _httpClientFactory.CreateClient();

        var reqBody = new GoogleAuthRequest
        {
            code = googleOIDCRequestParameters.Code,
            client_id = _configuration["Authentication:Google:ClientId"],
            client_secret = _configuration["Authentication:Google:ClientSecret"],
            redirect_uri = "http://localhost:5003/api/auth/google/login",
            grant_type = "authorization_code",
        };

        var serializedReqBody = new StringContent(
            JsonSerializer.Serialize(reqBody),
            Encoding.UTF8,
            MediaTypeNames.Application.Json
        );

        try
        {
            var googleOAuthResponse = await httpClient.PostAsync(
                "https://oauth2.googleapis.com/token", 
                serializedReqBody);
        
            var rawString = googleOAuthResponse.Content.ReadAsStringAsync().Result;
            var jsonResponse = JsonSerializer.Deserialize<GoogleAuthResponse>(rawString);
            var googleIdToken = jsonResponse?.IdToken;

            if (googleIdToken == null)
            {
                return BadRequest("Google ID token not found");
            }
            
            var userResult = await _googleAuthService.GoogleSignInAsync(googleIdToken);
            if (!userResult.Success)
            {
                return BadRequest(userResult.ErrorMessage);
            }
            
            var user = userResult.Value;
            
            /*
             * Set an HttpOnly cookie for the refresh token and return the Access token and other details back
             * in the response body along with redirecting.
             */
            //HttpCookie refreshTokenCookie = new HttpCookie("RefreshToken", user.RefreshToken);
            var res = new { AccessToken = user.AccessToken, RefreshToken = user.RefreshToken, Username = user.Username, Email = user.Email };
            var serializedRes = JsonSerializer.Serialize(res);
            
            Response.Cookies.Append("access_token", user.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Required if SameSite=None
                SameSite = SameSiteMode.Lax // Or SameSiteMode.None for cross-site
            });
            
            Response.Cookies.Append("refreshToken", user.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Required if SameSite=None
                SameSite = SameSiteMode.Lax, // Or SameSiteMode.None for cross-site
                Path = "/api/auth/refresh"
            });
            
            return Redirect("http://localhost:5173/kam");
            
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("google/signup")]
    public async Task<IActionResult> GoogleSignUp([FromQuery] GoogleOIDCRequestParametersDto googleOIDCRequestParameters)
    {
        var httpClient = _httpClientFactory.CreateClient();

        var reqBody = new GoogleAuthRequest
        {
            code = googleOIDCRequestParameters.Code,
            client_id = _configuration["Authentication:Google:ClientId"],
            client_secret = _configuration["Authentication:Google:ClientSecret"],
            redirect_uri = "http://localhost:5003/api/auth/google/signup",
            grant_type = "authorization_code",
        };

        var serializedReqBody = new StringContent(
            JsonSerializer.Serialize(reqBody),
            Encoding.UTF8,
            MediaTypeNames.Application.Json
        );

        try
        {
            var googleOAuthResponse = await httpClient.PostAsync(
                "https://oauth2.googleapis.com/token", 
                serializedReqBody);
        
            var rawString = googleOAuthResponse.Content.ReadAsStringAsync().Result;
            var jsonResponse = JsonSerializer.Deserialize<GoogleAuthResponse>(rawString);
            var googleIdToken = jsonResponse?.IdToken;

            if (googleIdToken == null)
            {
                return BadRequest("Google ID token not found");
            }
            
            var userExistsResult = await _googleAuthService.GoogleSignInAsync(googleIdToken);
            if (userExistsResult.Success)
            {
                return Redirect("http://localhost:5173/auth/login");
            }

            var completeProfileTokenResult = await _googleAuthService.GoogleSignUpAsync(googleIdToken);
            var completeProfileToken = completeProfileTokenResult.Value;

            var res = new { CompleteProfileToken = completeProfileToken.CompleteProfileToken };
            var serializedResponse = JsonSerializer.Serialize(res);
            
            Response.Cookies.Append("CompleteProfileToken", completeProfileToken.CompleteProfileToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // Required if SameSite=None
                SameSite = SameSiteMode.Lax,
                Path = "/api/auth/complete-profile"
            });
            
            return Redirect("http://localhost:5173/auth/complete-profile");
            
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("complete-profile")]
    public async Task<IActionResult> CompleteProfile([FromBody] CompletedUserProfileDto completedUserProfile)
    {
        if (!Request.Cookies.TryGetValue("CompleteProfileToken", out var completeProfileToken))
        {
            return RedirectToAction("http://localhost:5173/auth/login");
        }
        
        var createUserResult = await _completeProfileService.CompleteUserCreationAsync(completedUserProfile.ToModel(), completeProfileToken);
        var createdUser = createUserResult.Value;
        
        Response.Cookies.Append("AccessToken", createdUser.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
        });
        
        Response.Cookies.Append("RefreshToken", createdUser.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Path = "/api/auth/refresh"
        });
        
        Response.Cookies.Delete("CompleteProfileToken");
        
        return Created("http://localhost:5173", "");
    }
    
    [HttpGet("complete-profile")]
    public async Task<IActionResult> GetCurrentProfileCompletionStateAsync()
    {
        if (!Request.Cookies.TryGetValue("CompleteProfileToken", out var completeProfileToken))
        {
            return Unauthorized();
        }

        return Ok();
    }

    [HttpGet("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        if (!Request.Cookies.TryGetValue("AccessToken", out var accessToken))
        {
            return Unauthorized();
        }
        
        if (!Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
        {
            var accessTokenClaimsResult = _jwtService.GetAccessTokenClaims(accessToken);
            var accessTokenClaims = accessTokenClaimsResult.Value;
            var existingRefreshToken = await _refreshTokenService.GetRefreshTokenByUserIdAsync(accessTokenClaims.UserId);
            
            if (!existingRefreshToken.Success)
            {
                return Unauthorized();
            }
            
            var refreshTokenExpired = await _jwtService.IsTokenExpiredAsync(existingRefreshToken.Value.Token);
            if (refreshTokenExpired)
            {
                Response.Cookies.Delete("RefreshToken");
                return Unauthorized();
            }
            
            var user = await _userService.FindUserByIdAsync(accessTokenClaims.UserId);
            var newAccessToken = _jwtService.GenerateAccessToken(user.Value);
            
            Response.Cookies.Append("AccessToken", newAccessToken.EncodedToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
            });

            return Ok();
        }
        
        var refreshTokenClaimsResult = _jwtService.GetRefreshTokenClaims(refreshToken);
        var refreshTokenClaims = refreshTokenClaimsResult.Value;
        var fetchedUser = await _userService.FindUserByEmailAsync(refreshTokenClaims.Email);
        var refreshedAccessToken = _jwtService.GenerateAccessToken(fetchedUser.Value);
        
        Response.Cookies.Append("AccessToken", refreshedAccessToken.EncodedToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
        });

        return Ok();
    }
    
}