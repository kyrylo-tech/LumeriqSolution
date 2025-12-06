using Logic.Classes;

namespace WebAPI.Controllers.Auth;

public class AuthUserResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}


public static class AuthMapper
{
    public static AuthUserResponse ToResponse(this AuthUser user)
    {
        return new AuthUserResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
}
