using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.PnlGraph;

public interface IPnlGraphService {
  public Task<IEnumerable<PnlGraph>> getPnlGraph();
} 
