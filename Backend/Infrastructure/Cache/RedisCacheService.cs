using System.Text.Json;
using StackExchange.Redis;

namespace Backend.Infrastructure.Cache;

public interface IRedisCache
{
    Task SetAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default);
    Task<T?> GetAsync<T>(string key, CancellationToken ct = default);
    Task<bool> RemoveAsync(string key, CancellationToken ct = default);
    ISubscriber PubSub { get; }
}

public class RedisCacheService : IRedisCache
{
    private readonly IDatabase _db;
    public ISubscriber PubSub { get; }

    private static readonly JsonSerializerOptions JsonOpts = new(JsonSerializerDefaults.Web);

    public RedisCacheService(IConnectionMultiplexer mux)
    {
        _db = mux.GetDatabase();
        PubSub = mux.GetSubscriber();
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(value, JsonOpts);
        await _db.StringSetAsync(key, json, ttl);
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
    {
        var val = await _db.StringGetAsync(key);
        if (val.IsNullOrEmpty) return default;
        return JsonSerializer.Deserialize<T>((string)val!, JsonOpts);
    }

    public Task<bool> RemoveAsync(string key, CancellationToken ct = default)
        => _db.KeyDeleteAsync(key);
}