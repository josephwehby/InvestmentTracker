import OrderEntry from "../components/OrderEntry";
import PortfolioValue from "../components/PortfolioValue";
import Positions from "../components/Positions";
import { UnrealizedGainsProvider } from "../contexts/UnrealizedGainsContext";
import "../stylesheets/Home.css";

function Home() {
  return (
    <>
      <UnrealizedGainsProvider>
        <PortfolioValue />
        <OrderEntry />
        <Positions />  
      </UnrealizedGainsProvider>
    </>
  );
}

export default Home;