namespace Backend.Models;

public class Trade {
  public int id { get; set; }
  public Guid userid { get; set; }
  public string trade_type { get; set; }
  public string ticker { get; set; }
  public decimal shares { get; set; }
  public decimal price { get; set; }
  public decimal fees { get; set; }
  public DateTime purchase_day { get; set; }
  public DateTime? sell_day { get; set; }
}
