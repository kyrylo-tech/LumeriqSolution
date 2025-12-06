namespace Logic.Core.DbContext;

public static class PostgresUrlParser
{
    public static string Convert(string dbUrl)
    {
        if (string.IsNullOrWhiteSpace(dbUrl))
            return "";

        if (dbUrl.Contains("Host=", StringComparison.OrdinalIgnoreCase))
            return dbUrl;

        if (dbUrl.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) ||
            dbUrl.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
        {
            var uri = new Uri(dbUrl);

            var userInfo = uri.UserInfo.Split(':');
            var username = userInfo.ElementAtOrDefault(0) ?? "";
            var password = userInfo.ElementAtOrDefault(1) ?? "";

            var host = uri.Host;
            var port = uri.Port;

            var db = uri.LocalPath.TrimStart('/');

            return $"Host={host};Port={port};Database={db};Username={username};Password={password};SslMode=Require;Trust Server Certificate=True";
        }

        throw new FormatException("Unsupported DATABASE_URL format.");
    }
}