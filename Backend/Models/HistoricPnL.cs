namespace Backend.Models;

public class HistoricPnL {
  public int id { get; set; }
  public Guid userid { get; set; }
  public decimal pnl { get; set; }
  public DateTime closing_pnl_date { get; set; }
}
