using System.ComponentModel;
using Backend.Models;

namespace Backend.Services.Auth;

public interface IAuthService {
  public Task<string> Authenticate(LoginUser user);
  public Task<bool> Register(LoginUser user);
  public Task<string> Refresh(string refresh_token);
}