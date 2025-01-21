using koreawowtokenquotes.core;
using koreawowtokenquotes.core.Options;
using koreawowtokenquotes.core.DTOs.Res;

namespace koreawowtokenquotes.server.Services;

public class TokenService(
    HttpClient httpClient,
    IOptionsMonitor<BattleNetAuth> option,
    ICacheService cacheService) : ITokenService
{
    private HttpClient HttpClient => httpClient;
    private BattleNetAuth BattleNetAuth => option.CurrentValue;
    private ICacheService CacheService => cacheService;

    public async Task<string> GetTokenAsync()
    {
        string? token = CacheService.GetCachedValue(Consts.BattleNetAccessTokenKey);

        if (!string.IsNullOrWhiteSpace(token))
            return token;

        var clientId = BattleNetAuth.ClientId;
        var clientSecret = BattleNetAuth.ClientSecret;

        var tokenResponse = await HttpClient.PostAsync("https://oauth.battle.net/token", 
            new FormUrlEncodedContent([
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
            ]));

        var tokenData = await tokenResponse.Content.ReadFromJsonAsync<BattlenetAccessToken>();

        return CacheService.SetCachingValue(
            key: Consts.BattleNetAccessTokenKey,
            value: tokenData!.AccessToken,
            expiration: TimeSpan.FromHours(24)); // Battle API의 Access Token은 24시간동안 유효함
    }
}