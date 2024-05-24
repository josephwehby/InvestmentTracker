using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Services.Positions;
public interface IPositionService {
  public IEnumerable<Position> getAllPositions();
}