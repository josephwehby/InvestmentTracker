using Backend.Services.Auth;
using Backend.Models;
using System.Text;
using System;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Backend.Services.Auth;

public class AuthService : IAuthService {

  private readonly IConfiguration _config;

  public AuthService(IConfiguration config) {
    _config = config;
  }

  public string Authenticate(User user) {
    // convert password to bytes
    byte[] password_bytes = Encoding.ASCII.GetBytes(user.password);
    byte[] correct_bytes = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("INVESTMENT_TRACKER_KEY"));
    
    // hash with sha512 and compare
    byte[] password_hash;
    byte[] correct_hash;

    using (SHA512 sha512 = SHA512.Create()) { password_hash = sha512.ComputeHash(password_bytes); }

    using (SHA512 sha512 = SHA512.Create()) { correct_hash = sha512.ComputeHash(correct_bytes); }

    if (correct_hash.SequenceEqual(password_hash) && user.username == Environment.GetEnvironmentVariable("INVESTMENT_TRACKER_NAME")) {
      return GenerateToken(user);
    }

    return "";
  }

  private string GenerateToken(User user) {
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

}