#https://www.twilio.com/blog/containerize-your-aspdotnet-core-application-and-sql-server-with-docker
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebAppKey.csproj", "."]
RUN dotnet restore "./WebAppKey.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebAppKey.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAppKey.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAppKey.dll"]