using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Services.PnlGraph;

public interface IPnlGraphService {
  public Task<IEnumerable<HistoricPnLDto>> getPnlGraph();
} 
