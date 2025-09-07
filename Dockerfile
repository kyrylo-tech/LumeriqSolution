# ---- build stage ----
FROM mcr.microsoft.com/dotnet/sdk:9.0.101 AS build
WORKDIR /src

# кращий кеш restore
COPY Backend/Backend.csproj Backend/
RUN dotnet restore Backend/Backend.csproj

# тепер копіюємо все
COPY . .
RUN dotnet publish Backend/Backend.csproj -c Release -o /app/out --no-restore

# ---- runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_CLI_TELEMETRY_OPTOUT=1
ENTRYPOINT ["dotnet", "Backend.dll"]
