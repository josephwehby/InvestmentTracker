using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services.Auth;

namespace Backend.Controllers;

[ApiController]
[Route("auth")]
public class LoginController : ControllerBase {
  private readonly IAuthService _authService;

  public LoginController(IAuthService authService) {
    _authService = authService;
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginUser user) {
    
    Console.WriteLine("[!] Login attempt from " + HttpContext.Connection.RemoteIpAddress?.ToString());
    
    var (jwt, refresh) = await _authService.Authenticate(user);
    if (jwt == "") {
      return Unauthorized();
    } 
    
    // convert refresh token to cookie
    Console.WriteLine("[!] Authorized");
    return Ok(jwt);
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] LoginUser user) {
    bool register = await _authService.Register(user);
    return Unauthorized();
  }
}