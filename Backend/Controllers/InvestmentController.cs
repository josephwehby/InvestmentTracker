using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;
using Backend.Services.Positions;
using Backend.Services.Trades;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

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
  
  [HttpPost("add")]
  public ActionResult addTrade([FromBody] Trade trade) {
    bool result = _tradeService.addTrade(trade);
    if (result) {
      return Ok();
    }
    
    return StatusCode(500, "Error while adding trade");
  }

  [HttpGet("positions")]
  public ActionResult getAllPositions() {
    Console.WriteLine("[!] GET request");
    var positions = _positionService.getAllPositions();
    if (!positions.Any()) {
      return NoContent();
    }
    var temp = positions.ToList();
    var json = JsonConvert.SerializeObject(temp);
    return Ok(json);
  }
}