using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Services.ClosedPnLs;
public interface IClosedPnLService {
  public Task<decimal> getClosedPnL();
}