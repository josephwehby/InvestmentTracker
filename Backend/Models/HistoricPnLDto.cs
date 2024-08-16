namespace Backend.Models;

public class HistoricPnLDto {
  public decimal pnl { get; set; }
  public DateTime closing_pnl_date { get; set; }
}
