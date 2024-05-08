using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services.Investments;

namespace Backend.Controllers;

[ApiController]
[Route("investments")]
public class InvestmentController : ControllerBase {

  private readonly IInvestmentService _investmentService;

  public InvestmentController(IInvestmentService investmentService) {
    _investmentService = investmentService;
  }
  
  [HttpPost]
  public IActionResult createInvestment([FromBody] Investment investment) {
    _investmentService.createInvestment(investment);
    return Ok();
  }

  [HttpGet("{id}")]
  public IActionResult get(uint id) {
    Investment investment = _investmentService.getInvestment(id);
    if (investment == null) {
      return NotFound();
    }
    return Ok(investment);
  }

  [HttpPut("{id}")]
  public IActionResult updateInvestment(uint id)  {
    Investment investment = _investmentService.updateInvestment(id);
    if (investment == null) {
      return NotFound();
    }
    return Ok(investment);
  }

  [HttpDelete("{id}")]
  public IActionResult deleteInvestment(uint id)  {
    bool success = _investmentService.deleteInvestment(id);
    if (success) {
      return Ok();
    }

    return NotFound();
  }
}