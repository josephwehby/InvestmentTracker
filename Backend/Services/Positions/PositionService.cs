using Backend.Models;
using Backend.Services.Trades;

namespace Backend.Services.Positions;

public class PositionService : IPositionService {
  private readonly ITradeService _tradeService;

  public PositionService(ITradeService tradeService) {
    _tradeService = tradeService;
  }

  public async Task<IEnumerable<Position>> getAllPositions() {
    var trades = await _tradeService.getAllTrades();
    if (trades.Count() == 0) return Enumerable.Empty<Position>();

    var grouped_trades = trades.GroupBy(t => t.ticker);
    var positions = new List<Position>();
     
    foreach (var group in grouped_trades) {
      positions.Add(createPosition(group.ToList()));
    }

    return positions;
  }

  private Position createPosition(List<Trade> trades) {
    Position position = new Position {
      ticker = trades[0].ticker,
      current_price = 0,
      avg_cost = 0,
      quantity = 0,
      cost_basis = 0,
      market_value = 0,
      fees = 0,
      pnl = 0,
      percent_gain = 0
    };

    // api for this but hardcoding for now
    decimal current_shareprice = 100;

    foreach (var trade in trades) {
      position.quantity += trade.shares;
      position.pnl += (trade.shares*current_shareprice) - (trade.shares*trade.price);
      position.cost_basis += trade.shares*trade.price;
      position.market_value += trade.shares * current_shareprice;
      position.avg_cost += trade.price;
      position.fees += trade.fees;
    }
    
    position.current_price = current_shareprice;
    position.avg_cost = position.avg_cost / trades.Count;

    return position;
  }
}