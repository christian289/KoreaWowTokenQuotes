namespace koreawowtokenquotes.core;

public interface ITokenService
{
    Task<string> GetTokenAsync();
}
