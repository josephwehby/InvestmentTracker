using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Backend.Services.UserID;

public class UserService : IUserService {
  
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly ILogger _logger;

  public UserService(IHttpContextAccessor httpContextAccessor, ILogger<UserService> logger) {
    _httpContextAccessor = httpContextAccessor;
    _logger = logger;
  }

  public Guid getUserID() {
    var guid = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userid")?.Value;
    if (Guid.TryParse(guid, out var userId)) {
      return userId;
    }
    _logger.LogInformation("Error getting userid from jwt");
    throw new Exception("Invalid or missing user id.");
  }
}
