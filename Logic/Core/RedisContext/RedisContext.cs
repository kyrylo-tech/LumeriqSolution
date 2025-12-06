using StackExchange.Redis;

namespace Logic.Core.RedisContext;

public class RedisContext
{
    private readonly IConnectionMultiplexer _redis;
    public IDatabase Db { get; }
    
    public RedisContext(IConnectionMultiplexer redis)
    {
        _redis = redis;
        Db = _redis.GetDatabase();
    }
}