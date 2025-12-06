using System.ComponentModel.DataAnnotations;

namespace Logic.Classes;

public class AuthUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [MaxLength(50)] public string FirstName { get; set; } = string.Empty;
    [MaxLength(50)] public string LastName { get; set; } = string.Empty;
    
    [EmailAddress, MaxLength(50)] 
    public string Email { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
    // public string PasswordSalt { get; set; } = string.Empty;
    
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public static AuthUser Create(string firstName, string lastName, string email)
    {
        return new AuthUser {FirstName = firstName, LastName = lastName, Email = email};
    }
    
    public void ChangeEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail))
            throw new ArgumentException("email_can_not_be_empty");
        
        if (!newEmail.Contains("@"))
            throw new ArgumentException("email_is_not_valid");
        
        Email = newEmail;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void ChangePassword(string newHash)
    {
        Password = newHash;
        UpdatedAt = DateTime.UtcNow;
    }
}