import { useEffect, useState } from "react";
import { InvestmentPosition } from "../abstractions/InvestmentPosition";

function Positions() {
  const [positions, setPositions] = useState<InvestmentPosition[]>([]);
  
  async function getPositions() {
    const response = await fetch("https://localhost:5144/investments/positions");
    if (!response.ok) {
      throw new Error("Problem fetching api");
    }
    const data = await response.json();
    console.log(data);
    setPositions(data);
    
  }  
  
  useEffect(() => {
    getPositions();
  }, []);
  
  return (
    <div>
      <p>Positions</p>
    </div>
  );
}

export default Positions;