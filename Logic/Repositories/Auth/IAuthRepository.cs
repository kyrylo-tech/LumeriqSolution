using Logic.Classes;

namespace Logic.Repositories.Auth;

public interface IAuthRepository
{
    Task<AuthUser?> GetByEmail(string email);
    Task AddAsync(AuthUser user);
    Task UpdateAsync(AuthUser user);
}