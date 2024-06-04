import PortfolioValue from "../components/PortfolioValue";
import Positions from "../components/Positions";

function Home() {
  return (
    <>
      <PortfolioValue closed={2023.25} unrealized={-145.87} />
      <Positions />
    </>
  );
}

export default Home;