namespace Logic.Core.RedisContext;

public static class RedisUrlParser
{
    public static string Convert(string redisUrl)
    {
        if (string.IsNullOrWhiteSpace(redisUrl)) return "";
        
        var uri = new Uri(redisUrl);

        var host = uri.Host;
        var port = uri.Port;

        var userInfo = uri.UserInfo.Split(':');
        var password = userInfo.Length > 1 ? userInfo[1] : "";

        return $"{host}:{port},password={password},ssl=True,abortConnect=False";
    }
}
