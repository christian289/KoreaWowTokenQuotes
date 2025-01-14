namespace koreawowtokenquotes.server.DTOs.Res;

public record BattlenetAccessToken()
{
    [JsonPropertyName("access_token")]
    public required string AccessToken { get; init; }

    [JsonPropertyName("token_type")]
    public required string TokenType { get; init; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }

    [JsonPropertyName("sub")]
    public required string Sub { get; init; }
}