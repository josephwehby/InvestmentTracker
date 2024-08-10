using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services.Positions;
using Backend.Services.Trades;
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
  private readonly ILogger _logger;
  public InvestmentController(ITradeService tradeService, IPositionService positionService, IClosedPnLService closedService, ILogger<InvestmentController> logger) {
    _tradeService = tradeService;
    _positionService = positionService;
    _closedpnlservice = closedService;
    _logger = logger;
  }
  
  [HttpPost("add")]
  public async Task<ActionResult> addTrade([FromBody] Trade trade) {
    _logger.LogInformation("POST request for adding a trade.");
    bool result = await _tradeService.addTrade(trade);
    if (result) {
      return Ok();
    }
    _logger.LogInformation("Unable to add new trade"); 
    return StatusCode(500, "Error while adding trade");
  }

  [HttpGet("positions")]
  public async Task<ActionResult> getAllPositions() {
    _logger.LogInformation("GET request for positions.");
    var positions = await _positionService.getAllPositions();
    if (!positions.Any()) {
      return NoContent();
    }
    var temp = positions.ToList();
    var json = JsonConvert.SerializeObject(temp);
    return Ok(json);
  }

  [HttpGet("closed")]
  public async Task<ActionResult> getClosedPnL() {
    _logger.LogInformation("GET request for closed pnl.");
    var closed = await _closedpnlservice.getClosedPnL();
    return Ok(closed);
  }
}
