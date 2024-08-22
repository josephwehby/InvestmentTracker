using System.Net.Http;
using System.Net.Http.Headers;
namespace Backend.Services.Api;

public class ApiService : IApiService {
  private static readonly HttpClient _httpClient = new HttpClient {
    DefaultRequestHeaders = 
    {
      Authorization = new AuthenticationHeaderValue("Key", "ada"),
      Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
    }
  };

  public async Task<decimal> getPrice(string ticker) {
    string url = "https://api.tiingo.com/iex/" + ticker;
    decimal price = 0;

    try {
      var response = await _httpClient.GetAsync(url);
      
      if (response.IsSuccessStatusCode) {
        Console.WriteLine(response); 
      }

    } catch(HttpRequestException e) {
      
    } 

    return price;
  }
}
