using Backend.Models;

namespace Backend.Services.Auth;

public interface IAuthService {
  public string Authenticate(User user);
}