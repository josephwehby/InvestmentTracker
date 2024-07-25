using Backend.Data;
using Backend.Models;
using Backend.Services.UserID;

namespace Backend.Services.ClosedPnLs;

public class ClosedPnLService : IClosedPnLService {
  private readonly InvestmentsDbContext _context;
  private readonly IUserService _userService;

  public ClosedPnLService(InvestmentsDbContext context, IUserService userService) {
    _context = context;
    _userService = userService;
  }

  public async Task<decimal> getClosedPnL() {
    Guid userid = Guid.Empty;
    try {
      userid = _userService.getUserID();
    } catch (Exception) {
      return 0;
    }

    ClosedPnL closedpnl = await _context.ClosedPnL(userid);
    
    if (closedpnl == null) {
      return 0;
    }
    
    return closedpnl.pnl;
  }
}