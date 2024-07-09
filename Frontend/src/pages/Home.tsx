import {Link} from "react-router-dom";
import "../stylesheets/Home.css"

function Home() {
  return (
    <div className="home-screen">
      <h1>Welcome!</h1>
      <h2>Make sure to have a valid TLS certificate from the server to access the portfolio as this site uses mTLS</h2>
      <Link to={"/Portfolio"}>Portfolio</Link>
    </div>
  );
}

export default Home;