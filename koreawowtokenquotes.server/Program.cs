using koreawowtokenquotes.server.DTOs.Res;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

var clientId = builder.Configuration["BattleNet:ClientId"] ?? "your_battle_net_client_id";
var clientSecret = builder.Configuration["BattleNet:ClientSecret"] ?? "your_battle_net_client_secret";

app.MapGet("/api/game-data", async ([FromServices] IHttpClientFactory httpClientFactory) =>
{
    try
    {
        // HttpClient는 사실상 Http 메서드를 사용하기 위한 껍데기이기에 수명관리의 의미가 없다.
        // 정작 중요한 것은 HttpMessageHandler인데 HttpClientFactory를 이용해서 HttpClient를 사용할 경우
        //HttpMessageHandler가 유효한 경우 재사용 될 수 있기 때문에 CreateClient() 메서드가 의미가 있는 것이다.
        var client = httpClientFactory.CreateClient();

        // Battle net API 토큰 발급
        var tokenResponse = await client.PostAsync("https://oauth.battle.net/token", new FormUrlEncodedContent(
        [
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", clientSecret),
        ]));

        tokenResponse.EnsureSuccessStatusCode();
        var tokenData = await tokenResponse.Content.ReadFromJsonAsync<BattlenetAccessToken>();
        var accessToken = tokenData!.AccessToken;

        // Fetch game data
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        UriBuilder uriBuilder = new("https://kr.api.blizzard.com/data/wow/token/index")
        {
            Query = new QueryBuilder
            {
                { "namespace", "dynamic-kr" },
                { "locale", "ko_KR" }
            }.ToString()
        };
        var gameDataResponse = await client.GetAsync(uriBuilder.Uri);
        gameDataResponse.EnsureSuccessStatusCode();

        var gameData = await gameDataResponse.Content.ReadAsStringAsync();
        return Results.Ok(gameData);
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: ex.Message);
    }
});

app.Run();
