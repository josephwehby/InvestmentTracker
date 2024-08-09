import { useEffect, useState, useContext } from "react";
import { InvestmentPosition } from "../abstractions/InvestmentPosition";
import UnrealizedGainsContext from "../contexts/UnrealizedGainsContext";
import "../stylesheets/Positions.css";
import { useAuthContext } from "../contexts/AuthContext";
import { useReloadContext } from "../contexts/ReloadContext";
import apiClient from "../api/apiClient";

function Positions() {
  const [positions, setPositions] = useState<InvestmentPosition[]>([]);
  const context = useContext(UnrealizedGainsContext);
  const { reload } = useReloadContext();
  const { jwt } = useAuthContext(); 
  
  if (!context) {
    throw new Error("need unrealized gains provider");
  }

  
  const { setUnrealizedGains } = context;

  function getColor(value: number) {
    if (value > 0) {
      return "green";
    }
    return "red";
  }

  async function getPositions() {
    try {
      const response = await apiClient.get("/positions");
      const data = response.data;
      setPositions(data);  
      const totalUnrealizedGains = data.reduce((sum: number, position: InvestmentPosition) => sum + position.pnl, 0);
      setUnrealizedGains(totalUnrealizedGains);
      console.log("success");
    } catch (error) {
      setPositions([]);
      console.log("error");
    } 
  }  
  
  useEffect(() => {
    const timer = setInterval(() => {
      getPositions();
    }, 30*1000);  
    getPositions();
    return () => clearInterval(timer);
  }, [reload]);
  
  return (
    <div>
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
              <td> <span style={{color: getColor(position.pnl)}}>${position.pnl.toFixed(2)}</span> </td>
            </tr>
          ))}
        </tbody>
      </table> 
    </div>
  );
}

export default Positions;
