using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Domain.Entities;

[Table("IdentityUsers")]           
[Index(nameof(Email), IsUnique = true)]  
public class IdentityUser
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(255)] public string Email { get; set; } = null!;

    [Required] public byte[] PasswordHash { get; private set; } = Array.Empty<byte>();
    [Required] public byte[] PasswordSalt { get; private set; } = Array.Empty<byte>();

    
    [Required, MaxLength(255)] public string FirstName { get; set; } = null!;
    [Required, MaxLength(255)] public string LastName { get; set; } = null!;
    
    public DateOnly? BirthDate { get; set; }

    public bool IsEmailConfirmed { get; set; } = false;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}