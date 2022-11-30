FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore "./RedisAsDb.API/RedisAsDb.API.csproj" --disable-parallel
RUN dotnet publish "./RedisAsDb.API/RedisAsDb.API.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR app
COPY --from=build /app/out .

EXPOSE 80
EXPOSE 4000

ENV ASPNETCORE_ENVIRONMENT="Development"
ENV ASPNETCORE_HTTP_PORT="https://+:4001"
ENV ASPNETCORE_URLS="http://+:4000"

ENTRYPOINT ["dotnet", "RedisAsDb.API.dll"]