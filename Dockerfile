# ---- build ----
FROM mcr.microsoft.com/dotnet/sdk:9.0.101 AS build
WORKDIR /src

# кешуємо restore
COPY Backend/Backend.csproj Backend/
RUN dotnet restore Backend/Backend.csproj

# копіюємо все та публікуємо
COPY . .
RUN dotnet publish Backend/Backend.csproj -c Release -o /app/out /p:UseAppHost=false

# корисно: покажемо вивід, щоб бачитu ім'я dll
RUN ls -la /app/out

# ---- runtime ----
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out ./

ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_CLI_TELEMETRY_OPTOUT=1
EXPOSE 8080

# якщо назва dll невідома — запускаємо першу знайдену
ENTRYPOINT ["/bin/sh","-lc","dotnet *.dll"]
