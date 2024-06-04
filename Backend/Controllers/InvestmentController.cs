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
  
  [HttpPost]
  public ActionResult addTrade([FromBody] Trade trade) {
    _tradeService.addTrade(trade);
    return Ok();
  }

  [HttpGet("positions")]
  public ActionResult getAllPositions() {
    var positions = _positionService.getAllPositions();
    var temp = positions.ToList();
    var json = JsonConvert.SerializeObject(temp);
    Console.WriteLine("[!] GET REQUEST: " + temp[0].ticker + " " + temp[0].quantity);
    return Ok(json);
  }
}