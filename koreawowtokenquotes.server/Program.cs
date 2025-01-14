using koreawowtokenquotes.server.DTOs.Res;
using koreawowtokenquotes.server.interfaces;
using koreawowtokenquotes.server.Options;
using koreawowtokenquotes.server.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddHttpClient<ITokenService, TokenService>();
builder.Services.AddHttpClient();

builder.Services.Configure<BattleNetAuth>(builder.Configuration.GetSection(nameof(BattleNetAuth)));
builder.Services.Configure<BattleNetWoWTokenApiRequiredValue>(builder.Configuration.GetSection(nameof(BattleNetWoWTokenApiRequiredValue)));

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJS", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowNextJS");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapGet("/api/token-data", async ([FromServices] IHttpClientFactory httpClientFactory, ITokenService service) =>
{
    try
    {
        var accessToken = await service.GetTokenAsync();

        // HttpClient는 사실상 Http 메서드를 사용하기 위한 껍데기이기에 수명관리의 의미가 없다.
        // 정작 중요한 것은 HttpMessageHandler인데 HttpClientFactory를 이용해서 HttpClient를 사용할 경우
        //HttpMessageHandler가 유효한 경우 재사용 될 수 있기 때문에 CreateClient() 메서드가 의미가 있는 것이다.
        var client = httpClientFactory.CreateClient();

        // Fetch game data
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        UriBuilder uriBuilder = new("https://kr.api.blizzard.com/data/wow/token/index")
        {
            Query = new QueryBuilder
            {
                { "region", "kr" },
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
