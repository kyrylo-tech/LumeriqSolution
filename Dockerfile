# ---- build stage ----
FROM mcr.microsoft.com/dotnet/sdk:9.0.101 AS build
WORKDIR /src

# Кешуємо restore
COPY Backend/Backend.csproj Backend/
RUN dotnet restore Backend/Backend.csproj

# Копіюємо решту та публікуємо у /app/out
COPY . .
RUN dotnet publish Backend/Backend.csproj -c Release -o /app/out /p:UseAppHost=false
# Перевірка, що DLL справді зібрався
RUN ls -la /app/out

# ---- runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out ./

# Порт, який дає платформа
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
EXPOSE 8080

# Якщо впевнений у назві -> ["dotnet","Backend.dll"]
# Якщо назва може відрізнятися — запускаємо перший .dll у /app
ENTRYPOINT ["/bin/sh","-lc","dotnet *.dll"]
