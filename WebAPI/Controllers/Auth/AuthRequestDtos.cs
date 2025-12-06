using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers.Auth;

public class RegisterRequest
{
    [Required(ErrorMessage = "req_err_FirstName_Required")]
    [MaxLength(50, ErrorMessage = "req_err_FirstName_MaxLength")]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "req_err_LastName_Required")]
    [MaxLength(50, ErrorMessage = "req_err_LastName_MaxLength")]
    public string LastName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "req_err_Email_Required")]
    [EmailAddress(ErrorMessage = "req_err_Email_Invalid")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "req_err_Password_Required")]
    [MinLength(8, ErrorMessage = "req_err_Password_MinLength")]
    public string Password { get; set; } = string.Empty;
}

public class LoginRequest
{
    [Required(ErrorMessage = "req_err_Email_Required")]
    [EmailAddress(ErrorMessage = "req_err_Email_Invalid")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "req_err_Password_Required")]
    public string Password { get; set; } = string.Empty;
}


public class AuthResponse
{
    
}

// public class RegisterDto
// {
//     public string FirstName { get; set; } = string.Empty;
//     public string LastName { get; set; } = string.Empty;
//     public string Email { get; set; } = string.Empty;
//     public string Password { get; set; } = string.Empty;
// }
//
// public class LoginDto
// {
//     public string Email { get; set; } = string.Empty;
//     public string Password { get; set; } = string.Empty;
// }