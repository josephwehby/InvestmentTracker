using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Backend.Services.UserID;

public class UserService : IUserService {
  
  private readonly IHttpContextAccessor _httpContextAccessor;

  public UserService(IHttpContextAccessor httpContextAccessor) {
    _httpContextAccessor = httpContextAccessor;
  }

  public string getUserID() {
    return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userid")?.Value;
  }
}