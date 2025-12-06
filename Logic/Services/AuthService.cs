using Logic.Classes;
using Logic.Repositories.Auth;
using Microsoft.AspNetCore.Identity;

namespace Logic.Services;

public class AuthService
{
    private readonly IAuthRepository _repo;
    private readonly PasswordHasher<AuthUser> _hasher;

    public AuthService(IAuthRepository repo)
    {
        _repo = repo;
        _hasher = new PasswordHasher<AuthUser>();
    }

    public async Task<AuthUser> RegisterAsync(string email, string password)
    {
        var user = AuthUser.Create("Test", "Testovich", email);

        var hash = _hasher.HashPassword(user, password);
        user.ChangePassword(hash);

        await _repo.AddAsync(user);

        return user;
    }
    
    public async Task<AuthUser?> LoginAsync(string email, string password)
    {
        var user = await _repo.GetByEmail(email);
        if (user == null) return null;
        
        var valid = _hasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success;
        return valid ? user : null;
    }
}