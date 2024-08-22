using Backend.Models;
using Backend.Services.Trades;
using Backend.Services.Api;

namespace Backend.Services.Positions;

public class PositionService : IPositionService {
  private readonly ITradeService _tradeService;
  private readonly IApiService _apiService;

  public PositionService(ITradeService tradeService, IApiService apiService) {
    _tradeService = tradeService;
    _apiService = apiService;
  }

  public async Task<IEnumerable<Position>> getAllPositions() {
    var trades = await _tradeService.getAllTrades();
    if (trades.Count() == 0) return Enumerable.Empty<Position>();

    var grouped_trades = trades.GroupBy(t => t.ticker);
    var task_positions = new List<Task<Position>>();
     
    foreach (var group in grouped_trades) {
      var trade_task = group.ToList();
      var task = createPosition(trade_task);
      task_positions.Add(task);
    }

    var positions = await Task.WhenAll(task_positions);
    return positions;
  }

  private async Task<Position> createPosition(List<Trade> trades) {
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
    decimal current_shareprice = await _apiService.getPrice(trades[0].ticker);

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
