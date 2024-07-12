import "../stylesheets/Home.css"
import Login from "../components/Login";

function Home() {
  return (
    <div className="home-screen">
      <h1>Sign In</h1>
      <Login />
    </div>
  );
}

export default Home;