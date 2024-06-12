import { useEffect, useState, useContext } from "react";
import { InvestmentPosition } from "../abstractions/InvestmentPosition";
import UnrealizedGainsContext from "../contexts/UnrealizedGainsContext";

function Positions() {
  const [positions, setPositions] = useState<InvestmentPosition[]>([]);
  const context = useContext(UnrealizedGainsContext);

  if (!context) {
    throw new Error("need unrealized gains provider");
  }
  
  const { setUnrealizedGains } = context;

  async function getPositions() {
    const response = await fetch("https://localhost:7274/investments/positions");
    if (!response.ok) {
      throw new Error("Problem fetching api");
    }
    const data = await response.json();
    setPositions(data);  
    const totalUnrealizedGains = data.reduce((sum: number, position: InvestmentPosition) => sum + position.pnl, 0);
    setUnrealizedGains(totalUnrealizedGains);
  }  
  
  useEffect(() => {
    getPositions();
  }, []);
  
  return (
    <div>
      <p>Positions</p>
      <table>
        <thead>
          <tr>
            <th>Ticker</th>
            <th>Current Price</th>
            <th>Avg Cost</th>
            <th>Quantity</th>
            <th>Cost Basis</th>
            <th>Market Value</th>
            <th>Fees</th>
            <th>PnL</th>
          </tr>
        </thead>
        <tbody>
          {positions.map((position) => (
            <tr key={position.ticker}>
              <td>{position.ticker}</td>
              <td>${position.current_price.toFixed(2)}</td>
              <td>${position.avg_cost.toFixed(2)}</td>
              <td>{position.quantity.toFixed(2)}</td>
              <td>${position.cost_basis.toFixed(2)}</td>
              <td>${position.market_value.toFixed(2)}</td> 
              <td>${position.fees.toFixed(2)}</td>
              <td>${position.pnl.toFixed(2)}</td>
            </tr>
          ))}
        </tbody>
      </table> 
    </div>
  );
}

export default Positions;