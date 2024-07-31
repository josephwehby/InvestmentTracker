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

  public AuthService(IConfiguration config, IHttpContextAccessor httpContextAccessor, UserDbContext context, IUserService userservice) {
    _config = config;
    _httpContextAccessor = httpContextAccessor;
    _context = context;
    _userservice = userservice;
  }

  public async Task<(string, string)> Authenticate(LoginUser user) {
    var current_user = await _context.getUser(user.username);
    
    if (current_user == null) {
      return ("", "");
    }
    
    // combine salt and password
    byte[] salt_bytes = Convert.FromBase64String(current_user.salt);
    byte[] password_hash = generateHash(salt_bytes, user.password);

    byte[] current_password = Convert.FromBase64String(current_user.password_hash);

    if (current_password.SequenceEqual(password_hash)) {
      string access_token = GenerateToken(user, current_user.id, current_user.username);
      string refresh = GenerateRefreshToken();
      if (current_user.id.HasValue) {
        await setRefreshTokenCookie(refresh, current_user.id.Value);
      } else {
        Console.WriteLine("[!] Userid is not set");
        return ("", "");
      }
      return (access_token, refresh);
    }

    return ("", "");
  }
  
  public async Task setRefreshTokenCookie(string refresh_token, Guid userid) {
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


  public async Task<bool> Register(LoginUser user) {
    // check if user exists
    var exists = await _context.getUser(user.username);
    if (exists != null) {
      return false;
    }

    // create salt
    var rng = RandomNumberGenerator.Create();
    byte[] salt = new byte[16];
    rng.GetBytes(salt);

    byte[] hash = generateHash(salt, user.password);
    Console.WriteLine("Salt: " + Convert.ToBase64String(salt));
    Console.WriteLine("Password: " + Convert.ToBase64String(hash));

    var new_user = new User {
      username = user.username,
      password_hash = Convert.ToBase64String(hash),
      salt = Convert.ToBase64String(salt),
    };

    await _context.addUser(new_user);

    return true;
  }

  public async Task<(string, string)> Refresh(string refresh_token) {
    if (refresh_token == null) return ("", "");
    var userid = _userservice.getUserID();
    var user = await _context.getUserFromID(userid);
    
    // compare tokens and check expiration
    return("", "");
  }

  private string GenerateToken(LoginUser user, Guid? id, string username) {
    if (!id.HasValue) {
      Console.WriteLine("[!] Guid cannot be null");
      return "";
    }
    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JWT:Key"]));
    var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var token_options = new JwtSecurityToken(
      issuer: _config["JWT:Issuer"],
      audience: _config["JWT:Audience"],
      claims: new List<Claim>{
        new Claim("userid", id.ToString()),
        new Claim(JwtRegisteredClaimNames.Sub, username)
      },
      expires: DateTime.UtcNow.AddMinutes(15),
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