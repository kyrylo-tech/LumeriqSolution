# ---- build stage ----
FROM mcr.microsoft.com/dotnet/sdk:9.0.101 AS build
WORKDIR /src

# 1) Кешуємо restore по одному .csproj
COPY Backend/Backend.csproj Backend/
RUN dotnet restore Backend/Backend.csproj

# 2) Копіюємо решту репо і публікуємо
COPY . .
# /p:UseAppHost=false — щоб точно був .dll (а не платформиний exe)
RUN dotnet publish Backend/Backend.csproj -c Release -o /app/out /p:UseAppHost=false

# (опціонально) перевірка, що DLL реально є
RUN ls -la /app/out

# ---- runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out ./

ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_CLI_TELEMETRY_OPTOUT=1
EXPOSE 8080

# Якщо ти впевнений у назві DLL:
# ENTRYPOINT ["dotnet","Backend.dll"]

# Якщо не впевнений у назві (наприклад, <AssemblyName> змінений у .csproj):
ENTRYPOINT ["/bin/sh","-lc","dotnet *.dll"]
