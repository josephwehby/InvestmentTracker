using Backend.Models;
using Backend.Data;
using Backend.Services.UserID;
namespace Backend.Services.PnlGraph;

public class PnlGraphService : IPnlGraphService {
  
  private readonly  InvestmentsDbContext _context;
  private readonly IUserService _userservice;
  private readonly ILogger _logger;

  public PnlGraphService(InvestmentsDbContext context, IUserService userservice, ILogger<PnlGraphService> logger) {
    _context = context;
    _userservice = userservice;
    _logger = logger;
  }
  
  public async Task<IEnumerable<HistoricPnLDto>> getPnlGraph() {
    Guid userid = Guid.Empty;
    try {
      userid = _userservice.getUserID();
    } catch (Exception) {
      _logger.LogInformation("No userid provided to get the historic pnl.");
      return new List<HistoricPnLDto>();
    }

    var pnls =  await _context.getHistoricPnL(userid); 
        
    if (pnls == null) {
      _logger.LogInformation("Error when fetching the historic pnl.");
      return new List<HistoricPnLDto>();
    } 
    
    var pnlgraph = pnls.Select(p => new HistoricPnLDto {
      pnl = p.pnl,
      closing_pnl_date = p.closing_pnl_date
    });

    return pnlgraph;
  }
}
