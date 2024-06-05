import { useEffect, useState } from "react";
import { InvestmentPosition } from "../abstractions/InvestmentPosition";

function Positions() {
  const [positions, setPositions] = useState<InvestmentPosition[]>([]);
  
  async function getPositions() {
    const response = await fetch("https://localhost:7274/investments/positions");
    if (!response.ok) {
      throw new Error("Problem fetching api");
    }
    const data = await response.json();
    setPositions(data);  
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
              <td>{position.current_price}</td>
              <td>{position.avg_cost}</td>
              <td>{position.quantity}</td>
              <td>{position.cost_basis}</td>
              <td>{position.market_value}</td> 
              <td>{position.fees}</td>
              <td>{position.pnl}</td>
            </tr>
          ))}
        </tbody>
      </table> 
    </div>
  );
}

export default Positions;