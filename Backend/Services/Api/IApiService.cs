namespace Backend.Services.Api;

public interface IApiService {
  public Task<(decimal, decimal)> getPrice(string ticker);
}
