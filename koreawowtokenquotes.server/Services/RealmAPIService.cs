namespace koreawowtokenquotes.server.Services;

using koreawowtokenquotes.core.DTOs.Res;
using koreawowtokenquotes.core;

public class RealmAPIService(
    HttpClient httpClient,
    ITokenService tokenService,
    ICacheService cacheService) : IRealmAPIService
{
    private HttpClient HttpClient => httpClient;
    private ITokenService TokenService => tokenService;
    private ICacheService CacheService => cacheService;

    public async Task<ResRealmIndex> GetRealmsIndexAsync()
    {
        var accessToken = await TokenService.GetTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        UriBuilder uriBuilder = new("https://kr.api.blizzard.com/data/wow/realm/index")
        {
            Query = new QueryBuilder
            {
                { "region", "kr" },
                { "namespace", "dynamic-kr" },
                { "locale", "ko_KR" }
            }.ToQueryString().Value
        };
        var gameDataResponse = await HttpClient.GetAsync(uriBuilder.Uri);
        gameDataResponse.EnsureSuccessStatusCode();
        var realmindex = await gameDataResponse.Content.ReadFromJsonAsync<ResRealmIndex>();

        return CacheService.SetCachingValue(Consts.RealmIndex, realmindex!);
    }

    public async Task<ResRealm> GetRealmAsync(string realmSlug)
    {
        var accessToken = await TokenService.GetTokenAsync();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        UriBuilder uriBuilder = new("https://kr.api.blizzard.com/data/wow/realm/realmSlug")
        {
            Query = new QueryBuilder
            {
                { "region", "kr" },
                { "realmSlug", realmSlug },
                { "namespace", "dynamic-kr" },
                { "locale", "ko_KR" }
            }.ToQueryString().Value
        };
        var gameDataResponse = await HttpClient.GetAsync(uriBuilder.Uri);
        gameDataResponse.EnsureSuccessStatusCode();
        var realm = await gameDataResponse.Content.ReadFromJsonAsync<ResRealm>();

        return realm!;
    }
}