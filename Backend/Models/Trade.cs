namespace Backend.Models;

public class Trade {
  public uint id { get; set; }
  public string? trade_type { get; set; }
  public string? ticker { get; set; }
  public double shares { get; set; }
  public double buy_price { get; set; }
  public double sell_price { get; set; }
  public double fees { get; set; }
  public DateTime purchase_day { get; set; }
  public DateTime sell_day { get; set; }
}