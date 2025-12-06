FROM mcr.microsoft.comdotnetsdk9.0.101 AS build
WORKDIR src

COPY BackendBackend.csproj Backend
RUN dotnet restore BackendBackend.csproj

COPY . .
RUN dotnet publish BackendBackend.csproj -c Release -o appout pUseAppHost=false
RUN ls -la appout   # дебаг побачиш назву DLL у білд-логах

FROM mcr.microsoft.comdotnetaspnet9.0
WORKDIR app
COPY --from=build appout .

ENV ASPNETCORE_URLS=http0.0.0.0${PORT}
EXPOSE 8080

ENTRYPOINT [binsh,-lc,dotnet .dll]