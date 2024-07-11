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
    var token = _authService.Authenticate(user);
    if (token == "") {
      return Unauthorized();
    } 
    var jsonobject = new JObject();
    jsonobject.Add("token", token);
    var json = JsonConvert.SerializeObject(jsonobject);
    return Ok(json);
  }
}