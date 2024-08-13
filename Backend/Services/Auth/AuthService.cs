using Backend.Models;
using System.Text;
using Backend.Data;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Backend.Services.UserID;

namespace Backend.Services.Auth;

public class AuthService : IAuthService {

  private readonly IConfiguration _config;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly UserDbContext _context;
  private readonly IUserService _userservice;
  private readonly ILogger _logger;

  public AuthService(IConfiguration config, IHttpContextAccessor httpContextAccessor, UserDbContext context, IUserService userservice, ILogger<AuthService> logger) {
    _config = config;
    _httpContextAccessor = httpContextAccessor;
    _context = context;
    _userservice = userservice;
    _logger = logger;
  }

  public async Task<string> Authenticate(LoginUser user) {
    var current_user = await _context.getUser(user.username);
    
    if (current_user == null) {
     _logger.LogInformation("Login attempt failed as {Username} does not exist.", user.username); 
      return "";
    }
    
    // combine salt and password
    byte[] salt_bytes = Convert.FromBase64String(current_user.salt);
    byte[] password_hash = generateHash(salt_bytes, user.password);

    byte[] current_password = Convert.FromBase64String(current_user.password_hash);

    if (current_password.SequenceEqual(password_hash)) {
      string access_token = GenerateToken(current_user.id, current_user.username);
      string refresh = GenerateRefreshToken();
      if (current_user.id.HasValue) {
        await setRefreshTokenCookie(refresh, current_user.id.Value);
      } else {
        _logger.LogInformation("User id for {Username} is not set", user.username);
        return "";
      }
      _logger.LogInformation("User {Username} has logged in and a new jwt and refresh token have been created.", user.username);
      return access_token;
    }

    _logger.LogInformation("The password provided by {Username} does not match the one stored in the database.", user.username);
    return "";
  }
  
  public async Task<bool> Register(LoginUser user) {
    // check if user exists
    var exists = await _context.getUser(user.username);
    if (exists != null) {
      _logger.LogInformation("Register attempt failed as {Username} already exists", user.username);
      return false;
    }

    // create salt
    var rng = RandomNumberGenerator.Create();
    byte[] salt = new byte[16];
    rng.GetBytes(salt);

    byte[] hash = generateHash(salt, user.password);

    var new_user = new User {
      username = user.username,
      password_hash = Convert.ToBase64String(hash),
      salt = Convert.ToBase64String(salt),
    };

    await _context.addUser(new_user);
    _logger.LogInformation("Created new user {Username}", user.username);
    return true;
  }

  public async Task<string> Refresh(string refresh_token) {
    if (refresh_token == null) return "";
    
    var user = await _context.getUserFromRefreshToken(refresh_token);    
    if (user == null) {
      _logger.LogInformation("There is no user that has that refresh token");
      return "";
    }

    // compare tokens and check expiration
    var current_time = DateTime.UtcNow;

    if (current_time < user.token_expires) {
      string jwt = GenerateToken(user.id.Value, user.username);      
      string new_refresh_token = GenerateRefreshToken();
      await setRefreshTokenCookie(new_refresh_token, user.id.Value);
      _logger.LogInformation("New jwt and refresh token created for user {Username}", user.username);
      return jwt;      
    } 
    
    _logger.LogInformation("Refresh token for {Username} is expired.", user.username);
    return "";
  }

  public async Task<bool> Logout(string refresh_token) {
    if (refresh_token == null) {
      _logger.LogInformation("Refresh token is null");
      return false;
    }

    var user = await _context.getUserFromRefreshToken(refresh_token);    
    if (user == null) {
      _logger.LogInformation("There is no user that has that refresh token");
      return false;
    }

    await setRefreshTokenCookie("", user.id.Value);
    _logger.LogInformation("Refresh cookie has been set to an empty string");
    return true;
  }
  
  private async Task setRefreshTokenCookie(string refresh_token, Guid userid) {
    var created = DateTime.UtcNow;
    var expires = DateTime.UtcNow.AddDays(7);

    var cookieOptions = new CookieOptions {
      HttpOnly = true,
      Secure = true,
      SameSite = SameSiteMode.Lax,
      Expires = expires
    };

    await _context.setRefreshToken(userid, refresh_token, created, expires);
    _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refresh_token, cookieOptions);
  }

  private string GenerateToken(Guid? id, string username) {
    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JWT:Key"]));
    var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var token_options = new JwtSecurityToken(
      issuer: _config["JWT:Issuer"],
      audience: _config["JWT:Audience"],
      claims: new List<Claim>{
        new Claim("userid", id.ToString()),
        new Claim(JwtRegisteredClaimNames.Sub, username)
      },
      expires: DateTime.UtcNow.AddMinutes(20),
      signingCredentials: signinCredentials
    );

    var token = new JwtSecurityTokenHandler().WriteToken(token_options);
    return token;
  }

  private string GenerateRefreshToken() {
    var rng = RandomNumberGenerator.Create();
    
    byte[] bytes = new byte[64];
    rng.GetBytes(bytes);

    string refresh_token = Convert.ToBase64String(bytes);
    return refresh_token;
  }


  private byte[] generateHash(byte[] salt, string password) {
    
    byte[] password_bytes = Encoding.ASCII.GetBytes(password);
    byte[] password_and_salt = new byte[password_bytes.Length + salt.Length];
    
    Buffer.BlockCopy(password_bytes, 0, password_and_salt, 0, password_bytes.Length);
    Buffer.BlockCopy(salt, 0, password_and_salt, password_bytes.Length, salt.Length);    

    byte[] password_hash;
    using (SHA256 sha256 = SHA256.Create()) { password_hash = sha256.ComputeHash(password_and_salt); }

    return password_hash;
  }


}
