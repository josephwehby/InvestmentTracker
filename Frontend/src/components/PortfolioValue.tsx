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

  function getClosedPnL() {
    setClosed(100);
  }

  useEffect(() => {
    getClosedPnL();
  }, []);
  
  return (
    <div className="PortfolioValue">
      <div className="summary">
        <p>
          Unrealized: <span style= {{ color: getColor(unrealizedGains)}}>${unrealizedGains}</span>
        </p>
        <p>
          Closed: <span style= {{ color: getColor(closed)}}>${closed}</span>
          </p>
      </div>
    </div>
  );
}

export default PortfolioValue;