using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebChat.Api.Dtos.Auth;
using WebChat.Api.Policies;
using WebChat.Service.Services.Auth;
using WebChat.Api.Extensions.Mappers;

namespace WebChat.Api.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IGoogleAuthService _googleAuthService;
    private readonly ICompleteProfileService _completeProfileService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    
    public AuthenticationController(
        IGoogleAuthService googleAuthService, 
        ICompleteProfileService completeProfileService, 
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration
        )
    {
        _googleAuthService = googleAuthService;
        _completeProfileService = completeProfileService;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
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
            
            Response.Cookies.Append("completeProfileToken", completeProfileToken.CompleteProfileToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Required if SameSite=None
                SameSite = SameSiteMode.Lax, // Or SameSiteMode.None for cross-site
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
        if (!Request.Cookies.TryGetValue("CompleteProfile", out var completeProfileToken))
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
        
        return Created("http://localhost:5173", "");
    }
    
}