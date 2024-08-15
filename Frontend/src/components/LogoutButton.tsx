import { useNavigate } from "react-router-dom";
import authApiClient from "../api/authClient";
import "../stylesheets/Logout.css";

function LogoutButton() {
  
  async function logout() {
    try {
      const response = await authApiClient.post("/logout");
      localStorage.setItem("jwt", "");
      localStorage.setItem("username", "");
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
