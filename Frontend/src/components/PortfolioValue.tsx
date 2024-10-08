import "../stylesheets/PortfolioValue.css";
import UnrealizedGainsContext from "../contexts/UnrealizedGainsContext";
import { useReloadContext } from "../contexts/ReloadContext";
import { useState, useContext, useEffect } from "react";
import useAxios from "../hooks/useAxios";

function PortfolioValue() {
  const [closed, setClosed] = useState(0);
  const context = useContext(UnrealizedGainsContext);
  const { reload } = useReloadContext();
  const axiosInstance = useAxios();

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
    try {
      const response = await axiosInstance.get("/closed");
      if (response.status < 200 || response.status >= 300) {
        throw new Error("[!] Failed to fetch closesd pnl");
      }
      const pnl = response.data;
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
          Unrealized:&ensp; <span style= {{ color: getColor(unrealizedGains)}}>${unrealizedGains.toFixed(2)}</span>
        </p>
        <p>
          Closed:&ensp; <span style= {{ color: getColor(closed)}}>${closed.toFixed(2)}</span>
          </p>
      </div>
    </div>
  );
}

export default PortfolioValue;
