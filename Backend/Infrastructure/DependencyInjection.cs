using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Backend.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseNpgsql(cfg.GetConnectionString("Default"));
        });

        return services;
    }
    
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration cfg)
    {
        var cfgStr = cfg.GetSection("Redis")["Configuration"]!;
        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(cfgStr));
        return services;
    }

    // Якщо хочеш IDistributedCache-адаптер:
    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration cfg)
    {
        var cfgStr = cfg.GetSection("Redis")["Configuration"]!;
        services.AddStackExchangeRedisCache(o =>
        {
            o.Configuration = cfgStr;
            o.InstanceName = cfg.GetSection("Redis")["Instance"] ?? "lumeriq:";
        });
        return services;
    }
}