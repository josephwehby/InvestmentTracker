import OrderEntry from "../components/OrderEntry";
import PortfolioGraph from "../components/PortfolioGraph";
import PortfolioValue from "../components/PortfolioValue";
import Positions from "../components/Positions";
import Username  from "../components/Username";
import { ReloadContextProvider } from "../contexts/ReloadContext";
import { UnrealizedGainsProvider } from "../contexts/UnrealizedGainsContext";
import "../stylesheets/Portfolio.css";

function Portfolio() {
  return (
    <>
      <div className="main">
        <UnrealizedGainsProvider>
          <ReloadContextProvider>
            <div className="zero-row">
              <PortfolioValue />
              <Username />
            </div>
            <div className="first-row">
              <PortfolioGraph />
              <OrderEntry />
            </div>
            <div className="second-row">
              <Positions />  
            </div>
          </ReloadContextProvider>
        </UnrealizedGainsProvider>
      </div>
    </>
  );
}

export default Portfolio;
