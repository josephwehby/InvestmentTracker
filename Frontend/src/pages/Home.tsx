import OrderEntry from "../components/OrderEntry";
import PortfolioValue from "../components/PortfolioValue";
import Positions from "../components/Positions";
import { UnrealizedGainsProvider } from "../contexts/UnrealizedGainsContext";
import "../stylesheets/Home.css";

function Home() {
  return (
    <>
      <div className="main">
        <UnrealizedGainsProvider>
          <div className="first-row">
            <PortfolioValue />
            <OrderEntry />
          </div>
          <div className="second-row">
            <Positions />  
          </div>
        </UnrealizedGainsProvider>
      </div>
    </>
  );
}

export default Home;