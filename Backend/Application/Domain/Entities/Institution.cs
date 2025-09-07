using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Domain.Entities;

[Index(nameof(Slug), IsUnique = true)]
[Index(nameof(Domain), IsUnique = true)]
[Table("Institutions")]
public class Institution
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(255)]
    public string Name { get; set; } = null!;
    
    [Required, MaxLength(16)]
    public string ShortName { get; set; } = null!;

    [Required, MaxLength(255)]
    public string LegalName { get; set; } = null!;
    
    [MaxLength(24)] public string? Slug { get; set; }
    [MaxLength(255)] public string? Domain { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

}