using Backend.Models;
namespace Backend.Services.Investments;

public class InvestmentService : IInvestmentService {
  private static readonly Dictionary<uint, Investment> _investments = new();
  public void createInvestment(Investment investment) {
    _investments.Add(investment.id, investment);
  }

  public Investment getInvestment(uint id) {
    if (_investments.ContainsKey(id)) {
      return _investments[id];
    }
    return null;
  }

  public Investment updateInvestment(uint id) {
    if (_investments.ContainsKey(id)) {
      return _investments[id];
    } 
    return null;
  }

  public bool deleteInvestment(uint id) {
    if (_investments.ContainsKey(id)) {
      _investments.Remove(id);
      return true;
    }

    return false;
  }
}