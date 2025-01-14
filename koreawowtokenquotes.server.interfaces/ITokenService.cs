namespace koreawowtokenquotes.server.interfaces;

public interface ITokenService
{
    Task<string> GetTokenAsync();
}
