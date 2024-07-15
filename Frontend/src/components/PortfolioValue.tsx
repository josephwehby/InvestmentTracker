import "../stylesheets/PortfolioValue.css";
import UnrealizedGainsContext from "../contexts/UnrealizedGainsContext";
import ReloadContext from "../contexts/ReloadContext";
import { useState, useContext, useEffect } from "react";

function PortfolioValue() {
  const [closed, setClosed] = useState(0);
  const context = useContext(UnrealizedGainsContext);
  const reload_context = useContext(ReloadContext);

  if (!context) {
    throw new Error("need unrealized gains provider");
  }
  
  if (!reload_context) {
    throw new Error("new reload context");
  }

  const { unrealizedGains } = context;
  const { reload } = reload_context;

  function getColor(value: number) {
    if (value < 0) {
      return "red";
    }
    return "green";
  }

  async function getClosedPnL() {
    try {
      const token = localStorage.getItem("accessToken");
      const respone = await fetch("https://localhost:7274/investments/closed", {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      });
      if (!respone.ok) {
        throw new Error("[!] Failed to fetch closesd pnl");
      }
      const pnl = await respone.json();
      setClosed(pnl);
    } catch (error) {
      console.error("[!] Error fetching closed pnl");
    } 
  }

  useEffect(() => {
    getClosedPnL();
  }, [reload]);
  
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