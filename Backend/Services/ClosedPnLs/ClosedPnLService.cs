using Backend.Data;
using Backend.Models;
using Backend.Services.UserID;

namespace Backend.Services.ClosedPnLs;

public class ClosedPnLService : IClosedPnLService {
  private readonly InvestmentsDbContext _context;
  private readonly IUserService _userService;
  private readonly ILogger _logger;

  public ClosedPnLService(InvestmentsDbContext context, IUserService userService, ILogger<ClosedPnLService> logger) {
    _context = context;
    _userService = userService;
    _logger = logger;
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
      _logger.LogInformation("{userid}: unable to access closed pnl", userid);
      return 0;
    }
    _logger.LogInformation("{userid}: ${pnl}", userid, closedpnl.pnl);
    return closedpnl.pnl;
  }
}
