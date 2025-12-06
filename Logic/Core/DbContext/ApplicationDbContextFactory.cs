using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Logic.Core.DbContext;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        Env.Load(".env");
        var databaseUrl = Env.GetString("DATABASE_URL");
        
        optionsBuilder.UseNpgsql(databaseUrl);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}