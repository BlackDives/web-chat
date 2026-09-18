namespace WebChat.Api.Policies;

public static class AuthenticationPolicies
{
    public const string GoogleAuthScheme = "google_authentication_scheme";
    public const string BearerScheme = "bearer_scheme";
    public const string RefreshTokenScheme = "refresh_token_scheme";
    public const string CompleteProfileScheme = "complete_profile_scheme";
}