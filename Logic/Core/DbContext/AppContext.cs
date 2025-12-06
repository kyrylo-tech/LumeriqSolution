using Microsoft.EntityFrameworkCore;
using Logic.Classes;

namespace Logic
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AuthUser> Users { get; set; }
    }
}