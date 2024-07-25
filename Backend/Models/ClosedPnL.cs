namespace Backend.Models;

public class ClosedPnL {
  public int id { get; set; }
  public Guid userid { get; set; }
  public decimal pnl { get; set; }
}