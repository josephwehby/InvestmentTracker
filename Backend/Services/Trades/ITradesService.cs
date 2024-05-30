using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Services.Trades;
public interface ITradeService {
  void addTrade(Trade trade);
  void deleteTrade(uint id);
  IEnumerable<Trade> getAllTrades();
}