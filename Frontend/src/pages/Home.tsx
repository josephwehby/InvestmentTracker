import PortfolioValue from "../components/PortfolioValue";
import Positions from "../components/Positions";
import { UnrealizedGainsProvider } from "../contexts/UnrealizedGainsContext";
import "../stylesheets/Home.css";

function Home() {
  return (
    <>
      <UnrealizedGainsProvider>
        <PortfolioValue />
        <Positions />  
      </UnrealizedGainsProvider>
    </>
  );
}

export default Home;