using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Services.Investments;
public interface IInvestmentService {
  void createInvestment(Investment investment);
  Investment getInvestment(uint id);
  Investment updateInvestment(uint id);
  bool deleteInvestment(uint id);
}