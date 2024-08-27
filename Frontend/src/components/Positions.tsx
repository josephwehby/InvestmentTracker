import { useEffect, useState, useContext, useMemo } from "react";
import { InvestmentPosition } from "../abstractions/InvestmentPosition";
import UnrealizedGainsContext from "../contexts/UnrealizedGainsContext";
import "../stylesheets/Positions.css";
import { useReloadContext } from "../contexts/ReloadContext";
import useAxios from "../hooks/useAxios";

function Positions() {
  const [positions, setPositions] = useState<InvestmentPosition[]>([]);
  const [sortedField, setSortedField] = useState<string | null>(null);
  const [sortDirection, setSortDirection] = useState<'asc' | 'desc'>('asc');
  const context = useContext(UnrealizedGainsContext);
  const { reload } = useReloadContext();
  const axiosInstance = useAxios();

  if (!context) {
    throw new Error("need unrealized gains provider");
  }
 
  const { setUnrealizedGains } = context;

  function getColor(value: number) {
    if (value >= 0) {
      return "green";
    }
    return "red";
  }

  function handleSort(field: string) {
    if (field == sortedField) {
      setSortDirection(prev => prev === 'asc' ? 'desc' : 'asc');
    } else {
      setSortedField(field);
      setSortDirection('asc');
    }
  }

  async function getPositions() {
    try {
      const response = await axiosInstance.get("/positions");
      const data = response.data;
      setPositions(data);
      const totalUnrealizedGains = data.reduce((sum: number, position: InvestmentPosition) => sum + position.pnl, 0);
      setUnrealizedGains(totalUnrealizedGains);
    } catch (error) {
      setPositions([]);
      console.error("Error: " + error);
    } 
  }

  const sorted_positions = useMemo(
    function() {
      if (!sortedField) return positions;
      return positions.slice().sort(function(a, b) {
        const aval = a[sortedField];
        const bval = b[sortedField];

        if (aval < bval) {
          return sortDirection === 'asc' ? -1 : 1;
        } else if (aval > bval){
          return sortDirection === 'asc' ? 1 : -1;
        } else {
          return 0;
        }
      });
    }, [positions, sortedField, sortDirection]);
  
  useEffect(() => {
    const timer = setInterval(() => {
      getPositions();
    }, 5*60*1000);  
    getPositions();
    return () => clearInterval(timer);
  }, [axiosInstance, reload]);
  
  return (
    <div>
      <table>
        <thead>
          <tr>
            <th><button type="button" onClick={() => handleSort("ticker")}>Ticker</button></th>
            <th><button type="button" onClick={() => handleSort("current_price")}>Current Price</button></th>
            <th><button type="button" onClick={() => handleSort("price_day_difference")}>Daily Price Change</button></th>
            <th><button type="button" onClick={() => handleSort("avg_cost")}>Avg Cost</button></th>
            <th><button type="button" onClick={() => handleSort("quantity")}>Quantity</button></th>
            <th><button type="button" onClick={() => handleSort("cost_basis")}>Cost Basis</button></th>
            <th><button type="button" onClick={() => handleSort("market_value")}>Market Value</button></th>
            <th><button type="button" onClick={() => handleSort("fees")}>Fees</button></th>
            <th><button type="button" onClick={() => handleSort("pnl")}>PnL</button></th>
          </tr>
        </thead>
        <tbody>
          {sorted_positions.map((position) => (
            <tr key={position.ticker}>
              <td>{position.ticker}</td>
              <td><span style={{color: getColor(position.price_day_difference)}}>${position.current_price.toFixed(2)}</span></td>
              <td><span style={{color: getColor(position.price_day_difference)}}>{position.price_day_difference > 0 ? "+" : ""}{position.price_day_difference.toFixed(2)}</span></td>
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
