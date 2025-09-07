# ---- build stage ----
FROM mcr.microsoft.com/dotnet/sdk:9.0.101 AS build
WORKDIR /src

# кешуємо restore
COPY Backend/Backend.csproj Backend/
RUN dotnet restore Backend/Backend.csproj

# копіюємо все та публікуємо
COPY . .
RUN dotnet publish Backend/Backend.csproj -c Release -o /app/out /p:UseAppHost=false
RUN ls -la /app/out   # дебаг: побачиш назву DLL у білд-логах

# ---- runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out ./

# Railway підставить $PORT
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
EXPOSE 8080

# Назва DLL може відрізнятися — запускаємо першу
ENTRYPOINT ["/bin/sh","-lc","dotnet *.dll"]
