using Backend.Models;
using Backend.Services.Trades;

namespace Backend.Services.Positions;

public class PositionService : IPositionService {
  private readonly ITradeService _tradeService;
  
  public PositionService(ITradeService tradeService) {
    _tradeService = tradeService;
  }
  public IEnumerable<Position> getAllPositions() {
    var trades = _tradeService.getAllTrades();
    
    if (!trades.Any()) return Enumerable.Empty<Position>();

    var grouped_trades = trades.GroupBy(t => t.ticker);
    var positions = new List<Position>();
     
    foreach (var group in grouped_trades) {
      positions.Add(createPosition(group.ToList()));
    }

    return positions;
  }

  private Position createPosition(List<Trade> trades) {
    Position position = new Position();
    double quantity = 0;
    double unrealized_pnl = 0;      
    double avg_price = 0;
    double cost_basis = 0;
    double market_value = 0;
    double fees = 0;
    // api for this but harcoding for now
    double current_shareprice = 100.0;

    foreach (var trade in trades) {
      Console.WriteLine(trade.ticker + " " + trade.shares);
      quantity += trade.shares;
      unrealized_pnl += (trade.shares*current_shareprice) - (trade.shares*trade.buy_price);
      cost_basis += trade.shares*trade.buy_price;
      market_value += trade.shares * current_shareprice;
      avg_price += trade.buy_price;
      fees += trade.fees;
    }
    
    position.ticker = trades[0].ticker;
    position.current_price = current_shareprice;
    position.avg_cost = avg_price / trades.Count;
    position.quantity = quantity;
    position.cost_basis = cost_basis;
    position.market_value = market_value;
    position.fees = fees;
    position.pnl = unrealized_pnl;

    return position;
  }
}