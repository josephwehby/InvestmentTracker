namespace Backend.Models;

public class Trade {
  public int id { get; set; }
  public string trade_type { get; set; }
  public string ticker { get; set; }
  public decimal shares { get; set; }
  public decimal buy_price { get; set; }
  public decimal? sell_price { get; set; }
  public decimal fees { get; set; }
  public DateTime? sell_day { get; set; }
}