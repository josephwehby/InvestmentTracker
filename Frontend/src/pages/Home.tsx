import PortfolioValue from "../components/PortfolioValue";
import Positions from "../components/Positions";
import { UnrealizedGainsProvider } from "../contexts/UnrealizedGainsContext";

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