import { useNavigate } from "react-router-dom";
import authApiClient from "../api/authClient";
import "../stylesheets/Logout.css";
import { useAuthContext } from "../contexts/AuthContext"; 

function LogoutButton() {
  const navigate = useNavigate();   
  const { setIsAuthenticated } = useAuthContext();

  async function logout() {
    try {
      await authApiClient.post("/logout");
      localStorage.setItem("jwt", "");
      localStorage.setItem("username", "");
      setIsAuthenticated(false);
      navigate("/");
    } catch(error) {
      console.error("Error logging out: ",error);
    }
  }

  function handleButton() {
    logout();
  }

  return (
    <div className="logout-btn">
      <button type="button" onClick={handleButton}>Logout</button>
    </div>
  );
}

export default LogoutButton;
