using Logic.Core.RedisContext;
using Logic.Core.DbContext;
using Logic.Repositories.Auth;
using Logic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Logic;

public static class Extensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services, string redisUrl)
    {
        var connString = RedisUrlParser.Convert(redisUrl);

        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(connString));

        services.AddSingleton<RedisContext>();

        return services;
    }
    
    public static IServiceCollection AddAppData(this IServiceCollection services, string databaseUrl)
    {
        var connString = PostgresUrlParser.Convert(databaseUrl);

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connString));

        services.AddScoped<IAuthRepository, AuthRepository>();
        
        return services;
    }

    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<AuthService>();

        return services;
    }
}