using Backend.Data;
using Backend.Models;

namespace Backend.Services.ClosedPnLs;

public class ClosedPnLService : IClosedPnLService {
  private readonly InvestmentsDbContext _context;

  public ClosedPnLService(InvestmentsDbContext context) {
    _context = context;
  }

  public async Task<decimal> getClosedPnL() {
    ClosedPnL closedpnl = await _context.ClosedPnL();
    if (closedpnl == null) {
      return 0;
    }
    Console.WriteLine(closedpnl.pnl);
    return closedpnl.pnl;
  }
}