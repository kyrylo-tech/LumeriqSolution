using Backend.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<IdentityUser> Users => Set<IdentityUser>();
    public DbSet<Institution> Institutions => Set<Institution>();
    public DbSet<Member> Members => Set<Member>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);
    }
}