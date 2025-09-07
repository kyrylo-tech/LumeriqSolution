using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Application.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Domain.Entities;

[Index(nameof(UserId), nameof(InstitutionId), IsUnique = true)]
[Index(nameof(InstitutionId), nameof(Role))]
[Table("Members")]
public class Member
{
    public Guid Id { get; set; }
    public Guid InstitutionId { get; set; }
    public Guid UserId { get; set; }

    [Required] public MemberRole Role { get; set; }
    [Required] public bool IsArchived { get; set; } = false;
    
    public DateTimeOffset JoinedAt { get; set; } = DateTimeOffset.UtcNow;

    public IdentityUser User { get; set; } = null!;
    public Institution Institution { get; set; } = null!;
}