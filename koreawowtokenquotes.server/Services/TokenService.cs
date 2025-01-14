using koreawowtokenquotes.server.interfaces;
using koreawowtokenquotes.server.Options;
using koreawowtokenquotes.server.DTOs.Res;

namespace koreawowtokenquotes.server.Services;

public class TokenService(HttpClient httpClient, IOptionsMonitor<BattleNetAuth> option) : ITokenService
{
    private HttpClient HttpClient => httpClient;
    private BattleNetAuth BattleNetAuth => option.CurrentValue;

    public async Task<string> GetTokenAsync()
    {
        var clientId = BattleNetAuth.ClientId;
        var clientSecret = BattleNetAuth.ClientSecret;

        var tokenResponse = await HttpClient.PostAsync("https://oauth.battle.net/token", 
            new FormUrlEncodedContent([
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
            ]));

        var tokenData = await tokenResponse.Content.ReadFromJsonAsync<BattlenetAccessToken>();
        
        return tokenData!.AccessToken;
    }
}