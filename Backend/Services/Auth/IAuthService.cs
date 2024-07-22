using Backend.Models;

namespace Backend.Services.Auth;

public interface IAuthService {
  public (string, string) Authenticate(LoginUser user);
}