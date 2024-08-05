using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services.Auth;

namespace Backend.Controllers;

[ApiController]
[Route("auth")]
public class LoginController : ControllerBase {
  private readonly IAuthService _authService;
  private readonly ILogger _logger;

  public LoginController(IAuthService authService, ILogger<LoginController> logger) {
    _authService = authService;
    _logger = logger;
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginUser user) {
        
    var jwt = await _authService.Authenticate(user);
    if (jwt == "") {
      _logger.LogInformation("Failed login attempt from " + HttpContext.Connection.RemoteIpAddress?.ToString());
      return Unauthorized();
    } 
    
    // convert refresh token to cookie
    _logger.LogInformation("Successful login attempt from " + HttpContext.Connection.RemoteIpAddress?.ToString());
    return Ok(jwt);
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] LoginUser user) {
    bool register = await _authService.Register(user);
    
    if(!register) {
      _logger.LogInformation("Account registration failed");
      return BadRequest("Registration failed");
    }
    
    _logger.LogInformation("New user created: " + user.username);
    return Ok("user created");
  }

  [HttpGet("refresh")]
  public async Task<IActionResult> Refresh() {
    var refresh_token = HttpContext.Request.Cookies["refreshToken"];
    
    if (refresh_token == null) {
      _logger.LogInformation("No refresh token cookie provided.");
      return Unauthorized();
    }

    var jwt = await _authService.Refresh(refresh_token);   
    
    if (jwt == "") {
      _logger.LogInformation("Refresh token is not valid");
      return Unauthorized();
    }

    return Ok(jwt);
  }
}
