using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Backend.Services.Api;

public class ApiService : IApiService {
  
  private static readonly HttpClient _httpClient = new HttpClient {
    DefaultRequestHeaders = 
    {
      Authorization = new AuthenticationHeaderValue("Key", Environment.GetEnvironmentVariable("TIINGO-KEY")),
      Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
    }
  };

  public async Task<decimal> getPrice(string ticker) {
    string url = "https://api.tiingo.com/iex/" + ticker;
    decimal price = 0;

    try {
        string response = await _httpClient.GetStringAsync(url);
        //var parsed = Newtonsoft.JsonConvert.DeserializeObject(result);
        Console.WriteLine(response);


    } catch(HttpRequestException e) {

    } 

    return price;
  }
}
