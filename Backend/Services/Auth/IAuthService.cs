using Backend.Models;

namespace Backend.Services.Auth;

public interface IAuthService {
  public Task<(string, string)> Authenticate(LoginUser user);
  public Task setRefreshTokenCookie(string refresh_token, Guid userid);
  public Task<bool> Register(LoginUser user);
}