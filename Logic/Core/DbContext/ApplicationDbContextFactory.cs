using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Logic.Core.DbContext;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        var root = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        var envPath = Path.Combine(root, ".env");

        Env.Load(envPath);

        var databaseUrl = Env.GetString("DATABASE_URL");
        
        optionsBuilder.UseNpgsql(databaseUrl);
        
        // optionsBuilder.UseNpgsql("Host=localhost;Database=lumeriq;Username=postgres;Password=localhost");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}