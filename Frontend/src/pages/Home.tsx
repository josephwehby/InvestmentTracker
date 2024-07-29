import {Link} from "react-router-dom";
import "../stylesheets/Home.css";

function Home() {
  return(
    
    <div className="home-page">
      <div className="message">
        <h1>Welcome</h1>
      </div>
      <div className="login-link">
        <Link to="/login" className="link-group">Login</Link>
      </div>
      <div className="register-link">
        <Link to="/register" className="link-group">Register</Link>
      </div>
    </div>

  );
}

export default Home;