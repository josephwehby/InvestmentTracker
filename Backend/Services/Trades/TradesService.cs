using System.Transactions;
using Backend.Data;
using Backend.Models;
namespace Backend.Services.Trades;

public class TradeService : ITradeService {
  
  private readonly InvestmentsDbContext _context;
  
  public TradeService(InvestmentsDbContext context) {
    _context = context;
  }

  public async Task<bool> addTrade(Trade trade) {
    if (trade.trade_type == "buy") {
      try {
        trade.purchase_day = DateTime.UtcNow;
        _context.trades.Add(trade);
        await _context.SaveChangesAsync();
        Console.WriteLine("[!] Added Trade for ticker " + trade.ticker + " at " + trade.price + " for " + trade.shares + " shares");
        return true;
      } catch (Exception e) {
        Console.WriteLine("[!] Error " + e);
        return false;
      }
    }

    decimal sell = trade.price;    
    decimal total_shares = 0;
    var trades = await _context.getTradesByTicker(trade.ticker);

    // get total shares available
    foreach (var t in trades)  total_shares += t.shares;        
    
    if (total_shares < trade.shares) return false;

    var to_delete = new List<Trade>();  
    decimal remaining_shares = trade.shares;
    decimal profit = 0;

    foreach (var t in trades) {
      if (remaining_shares == 0) break;
      if (t.shares <= remaining_shares) {
        profit += (sell- t.price) * t.shares;
        to_delete.Add(t); 
        remaining_shares -= t.shares;       
      } else {
        profit += (sell - t.price) * remaining_shares;
        await _context.updateTradeShares(t.id, t.shares - remaining_shares);
        remaining_shares = 0;
        break;
      }
    }
    
    Console.WriteLine(profit);

    foreach (var trade_to_delete in to_delete) await _context.deleteTrade(trade_to_delete);
    
    return true;
  }

  public void deleteTrade(uint id) {
  }

  public IEnumerable<Trade> getAllTrades() {
    return _context.trades.ToList();
  }
  
}