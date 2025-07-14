public record UserTokenResponse
{
    // public required string RefreshToken;
    public required string AccessToken { get; set; }
}
