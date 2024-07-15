using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services.Auth;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Backend.Controllers;

[ApiController]
[Route("login")]
public class LoginController : ControllerBase {
  private readonly IAuthService _authService;

  public LoginController(IAuthService authService) {
    _authService = authService;
  }

  [HttpPost]
  public IActionResult Login([FromBody] User user) {
    Console.WriteLine("[!] Login attempt from " + HttpContext.Connection.RemoteIpAddress?.ToString());
    var token = _authService.Authenticate(user);
    if (token == "") {
      return Unauthorized();
    } 
    var response = new Token { jwt = token };
    return Ok(response);
  }
}