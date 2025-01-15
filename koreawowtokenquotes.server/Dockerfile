# DockerFile 위치 -> koreawowtokenquotes.server\Dockerfile
# docker build 명령어 실행 위치(build context) -> koreawowtokenquotes
# docker build 명령어 -> docker build -t wow-token-api -f koreawowtokenquotes.server/Dockerfile .

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ./koreawowtokenquotes.server/ koreawowtokenquotes.server/
COPY ./koreawowtokenquotes.server.interfaces/ koreawowtokenquotes.server.interfaces/
WORKDIR /src/koreawowtokenquotes.server
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5298
ENTRYPOINT ["dotnet", "koreawowtokenquotes.server.dll"]
