using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class InvestmentController : ControllerBase {
  
  [HttpPost("investments")]
  public IActionResult createInvestment([FromBody] Investment investment) {
    return Ok();
  }

  [HttpGet("investments")]
  public IActionResult getInvestment() {
    return Ok();
  }

  [HttpPut("investments/{id}")]
  public IActionResult updateInvestment(uint id)  {
    return Ok();
  }

  [HttpDelete("investments/{id}")]
  public IActionResult deleteInvestment(uint id)  {
    return Ok();
  }
  
}