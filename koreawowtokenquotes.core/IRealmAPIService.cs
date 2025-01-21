namespace koreawowtokenquotes.core;

using koreawowtokenquotes.core.DTOs.Res;

public interface IRealmAPIService
{
    Task<ResRealmIndex> GetRealmsIndexAsync();

    Task<ResRealm> GetRealmAsync(string realmSlug);
}
