using System.Text.Json;
using Logic.Core.RedisContext;
using StackExchange.Redis;

namespace Logic.Services;

public class RedisService
{
    private readonly IDatabase _db;
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public RedisService(RedisContext context)
    {
        _db = context.Db;
    }

    private static string BuildKey(string prefix, string key)
        => $"{prefix}:{key}";

    public async Task SetAsync<T>(string prefix, string key, T value, TimeSpan? ttl = null)
    {
        var json = JsonSerializer.Serialize(value, _jsonOptions);
        await _db.StringSetAsync(
            key: (RedisKey)BuildKey(prefix, key),
            value: (RedisValue)json,
            expiry: ttl ?? TimeSpan.FromDays(1)
        );
    }

    public async Task<T?> GetAsync<T>(string prefix, string key)
    {
        var result = await _db.StringGetAsync(BuildKey(prefix, key));

        if (result.IsNullOrEmpty)
            return default;

        return JsonSerializer.Deserialize<T>((string)result!, _jsonOptions);
    }

    public async Task<bool> RemoveAsync(string prefix, string key)
    {
        return await _db.KeyDeleteAsync(BuildKey(prefix, key));
    }

    public async Task<bool> ExistsAsync(string prefix, string key)
    {
        return await _db.KeyExistsAsync(BuildKey(prefix, key));
    }
}
