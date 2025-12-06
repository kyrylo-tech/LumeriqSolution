using Logic.Classes;
using Microsoft.EntityFrameworkCore;

namespace Logic.Repositories.Auth;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _ctx;
    
    public AuthRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<AuthUser?> GetByEmail(string email)
    {
        return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddAsync(AuthUser user)
    {
        await _ctx.Users.AddAsync(user);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(AuthUser user)
    {
        _ctx.Users.Update(user);
        await _ctx.SaveChangesAsync();
    }
}