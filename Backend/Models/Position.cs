namespace Backend.Models;

// this will be returned to the frontend

public class Position {
  public string ticker;
  public decimal current_price;
  public decimal avg_cost;
  public decimal quantity;
  public decimal cost_basis;
  public decimal market_value;
  public decimal fees;
  public decimal pnl;
  public decimal percent_gain;
}