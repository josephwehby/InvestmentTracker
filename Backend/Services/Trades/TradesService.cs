using Backend.Data;
using Backend.Models;
namespace Backend.Services.Trades;

public class TradeService : ITradeService {
  
  private readonly InvestmentsDbContext _context;
  
  public TradeService(InvestmentsDbContext context) {
    _context = context;
  }

  public async void addTrade(Trade trade) {
    try {
      _context.trades.Add(trade);
      await _context.SaveChangesAsync();
      Console.WriteLine("[!] Added Trade for ticker " + trade.ticker + " at " + trade.buy_price + " for " + trade.shares + " shares");
    } catch (Exception e) {
      Console.WriteLine("[!] Error " + e);
    }
  }

  public void deleteTrade(uint id) {
  }

  public IEnumerable<Trade> getAllTrades() {
    return _context.trades.ToList();
  }
  
}