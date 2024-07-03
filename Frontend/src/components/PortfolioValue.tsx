import "../stylesheets/PortfolioValue.css";
import UnrealizedGainsContext from "../contexts/UnrealizedGainsContext";
import { useState, useContext, useEffect } from "react";

function PortfolioValue() {
  const [closed, setClosed] = useState(0);
  const context = useContext(UnrealizedGainsContext);
  
  if (!context) {
    throw new Error("need unrealized gains provider");
  }
  
  const { unrealizedGains } = context;

  function getColor(value: number) {
    if (value < 0) {
      return "red";
    }
    return "green";
  }

  async function getClosedPnL() {
    const respone = await fetch("https://localhost:7274/investments/closed");
    console.log(respone);
  }


  useEffect(() => {
    getClosedPnL();
  }, []);
  
  return (
    <div className="PortfolioValue">
      <div className="summary">
        <p>
          Unrealized: <span style= {{ color: getColor(unrealizedGains)}}>${unrealizedGains.toFixed(2)}</span>
        </p>
        <p>
          Closed: <span style= {{ color: getColor(closed)}}>${closed.toFixed(2)}</span>
          </p>
      </div>
    </div>
  );
}

export default PortfolioValue;