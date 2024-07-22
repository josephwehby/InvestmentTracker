namespace Backend.Models;

public class User {
  public Guid? id { get; set; }
  public string username { get; set; }
  public string password_hash { get; set; }
  public string salt { get; set; }
  public string? refresh_token { get; set; }
  public DateTime? token_created { get; set; }
  public DateTime? token_expires { get; set; }
}