using Backend.Models;
namespace Backend.Services.Trades;

public class TradeService : ITradeService {
  
  private static readonly List<Trade> _trades = new();
  
  public void addTrade(Trade trade) {
    _trades.Add(trade);
  }

  public void deleteTrade(uint id) {
    for (int i = 0; i  < _trades.Count; i++) {
      if (_trades[i].id == id) {
        _trades.RemoveAt(i);
        break;
      }
    }
  }

  public IEnumerable<Trade> getAllTrades() {
    return _trades;
  }
  
}