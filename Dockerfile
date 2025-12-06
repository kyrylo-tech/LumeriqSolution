FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build

WORKDIR /src

COPY WebAPI/WebAPI.csproj WebAPI/
COPY Logic/Logic.csproj Logic/

RUN dotnet restore WebAPI/WebAPI.csproj

COPY . .

RUN dotnet publish WebAPI/WebAPI.csproj -c Release -o /app/out /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS runtime

WORKDIR /app

COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

EXPOSE 8080

ENTRYPOINT ["dotnet", "WebAPI.dll"]
