namespace Backend.Models;

// this will be returned to the frontend

public class Position {
  public string ticker {get; set; }
  public decimal current_price { get; set; }
  public decimal avg_cost { get; set; }
  public decimal quantity { get; set; }
  public decimal cost_basis { get; set; }
  public decimal market_value { get; set; }
  public decimal fees { get; set; }
  public decimal pnl { get; set; }
  public decimal percent_gain { get; set; }
  public decimal price_day_difference { get; set; }
}
