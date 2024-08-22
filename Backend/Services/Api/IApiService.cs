namespace Backend.Services.Api;

public interface IApiService {
  public Task<decimal> getPrice(string ticker);
}
