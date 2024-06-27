import OrderEntry from "../components/OrderEntry";
import PortfolioGraph from "../components/PortfolioGraph";
import PortfolioValue from "../components/PortfolioValue";
import Positions from "../components/Positions";
import { ReloadContextProvider } from "../contexts/ReloadContext";
import { UnrealizedGainsProvider } from "../contexts/UnrealizedGainsContext";
import "../stylesheets/Home.css";

function Home() {
  return (
    <>
      <div className="main">
        <UnrealizedGainsProvider>
          <ReloadContextProvider>
            <PortfolioValue />
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

export default Home;