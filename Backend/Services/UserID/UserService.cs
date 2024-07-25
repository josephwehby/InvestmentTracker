using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Backend.Services.UserID;

public class UserService : IUserService {
  
  private readonly IHttpContextAccessor _httpContextAccessor;

  public UserService(IHttpContextAccessor httpContextAccessor) {
    _httpContextAccessor = httpContextAccessor;
  }

  public Guid getUserID() {
    var guid = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userid")?.Value;
    if (Guid.TryParse(guid, out var userId)) {
      return userId;
    }
    throw new Exception("Invalid or missing user id.");
  }
}