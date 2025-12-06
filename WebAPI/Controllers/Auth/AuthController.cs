using Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;
    
    public AuthController(AuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest dto)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        var user = await _auth.RegisterAsync(dto.Email, dto.Password);
        return Ok(user.ToResponse());
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest dto)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        var user = await _auth.LoginAsync(dto.Email, dto.Password);
        
        if (user == null) 
            return Unauthorized("resp_err_InvalidCredentials");
        
        return Ok(user.ToResponse());
    }
}