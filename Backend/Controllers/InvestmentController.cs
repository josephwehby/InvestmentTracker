using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;
using Backend.Services.Positions;
using Backend.Services.Trades;

namespace Backend.Controllers;

[ApiController]
[Route("investments")]
public class InvestmentController : ControllerBase {

  private readonly ITradeService _tradeService;
  private readonly IPositionService _positionService;

  public InvestmentController(ITradeService tradeService, IPositionService positionService) {
    _tradeService = tradeService;
    _positionService = positionService;
  }
  
  [HttpPost]
  public IActionResult addTrade([FromBody] Trade trade) {
    _tradeService.addTrade(trade);
    return Ok();
  }

  [HttpGet("positions")]
  public IActionResult getAllPositions() {
    var positions = _positionService.getAllPositions();
    return Ok(positions);
  }
}