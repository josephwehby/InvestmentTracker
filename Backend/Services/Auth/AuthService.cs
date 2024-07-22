using Backend.Models;
using System.Text;
using Backend.Data;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.Services.Auth;

public class AuthService : IAuthService {

  private readonly IConfiguration _config;
  private readonly UserDbContext _context;

  public AuthService(IConfiguration config, UserDbContext context) {
    _config = config;
    _context = context;
  }

  public async Task<(string, string)> Authenticate(LoginUser user) {
    // convert password to bytes
    var current_user = await _context.getUser(user.username);
    
    if (current_user == null) {
      return ("", "");
    }
    
    // combine salt and password
    byte[] salt_bytes = Convert.FromBase64String(current_user.salt);
    byte[] password_hash = generateHash(salt_bytes, user.password);

    byte[] current_password = Convert.FromBase64String(current_user.password_hash);

    if (current_password.SequenceEqual(password_hash)) {
      string access_token = GenerateToken(user);
      string refresh = GenerateRefreshToken();
      return (access_token, refresh);
    }

    return ("", "");
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

  private string GenerateToken(LoginUser user) {
    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JWT:Key"]));
    var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var token_options = new JwtSecurityToken(
      issuer: _config["JWT:Issuer"],
      audience: _config["JWT:Audience"],
      claims: new List<Claim>(),
      expires: DateTime.Now.AddMinutes(15),
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