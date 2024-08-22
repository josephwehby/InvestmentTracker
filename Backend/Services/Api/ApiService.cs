using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Backend.Services.Api;

public class ApiService : IApiService {
  
  private static readonly HttpClient _httpClient = new HttpClient {
    BaseAddress = new Uri("https://api.tiingo.com/iex/"),
    DefaultRequestHeaders = 
    {
      Authorization = new AuthenticationHeaderValue("Token", Environment.GetEnvironmentVariable("TIINGO-KEY")),
      Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
    }
  };

  public async Task<decimal> getPrice(string ticker) {
    decimal price = 0;

    try {
        var response = await _httpClient.GetAsync(ticker);
        string responseBody =  await response.Content.ReadAsStringAsync();
        
        using (var doc = JsonDocument.Parse(responseBody)) {
          var root = doc.RootElement;
          var first = root[0];

          price = first.GetProperty("last").GetDecimal();

          return price;
        }
    } catch(HttpRequestException ex) {
        Console.WriteLine($"Error while getting stock quotes: {ex}");
        throw;
    } catch (JsonException ex) {
        Console.WriteLine($"Error parsing json: {ex}");
        throw;
    } catch (Exception e) {
        Console.WriteLine(e);
    }

    return price;
  }
}
