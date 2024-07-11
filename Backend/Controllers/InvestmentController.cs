using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;
using Backend.Services.Positions;
using Backend.Services.Trades;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using Backend.Services.ClosedPnLs;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers;

[Authorize]
[ApiController]
[Route("investments")]
public class InvestmentController : ControllerBase {

  private readonly ITradeService _tradeService;
  private readonly IPositionService _positionService;
  private readonly IClosedPnLService _closedpnlservice;

  public InvestmentController(ITradeService tradeService, IPositionService positionService, IClosedPnLService closedService) {
    _tradeService = tradeService;
    _positionService = positionService;
    _closedpnlservice = closedService;
  }
  
  [HttpPost("add")]
  public async Task<ActionResult> addTrade([FromBody] Trade trade) {
    Console.WriteLine("[!] POST request");
    bool result = await _tradeService.addTrade(trade);
    if (result) {
      return Ok();
    }
    
    return StatusCode(500, "Error while adding trade");
  }

  [HttpGet("positions")]
  public ActionResult getAllPositions() {
    Console.WriteLine("[!] GET all positions request");
    var positions = _positionService.getAllPositions();
    if (!positions.Any()) {
      return NoContent();
    }
    var temp = positions.ToList();
    var json = JsonConvert.SerializeObject(temp);
    return Ok(json);
  }

  [HttpGet("closed")]
  public async Task<ActionResult> getClosedPnL() {
    Console.WriteLine("[!] GET closed pnl request");
    var closed = await _closedpnlservice.getClosedPnL();
    return Ok(closed);
  }
}