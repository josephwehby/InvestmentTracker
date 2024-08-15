import "../stylesheets/Username.css";
import LogoutButton from "./LogoutButton";

function Username() {
  const name = localStorage.getItem("username") || "Default";
  return (
    <div className="user-box">
      <div className="username-display">
        <p>User: &ensp;<span style={{color: "green"}}>{name}</span></p>
      </div>
      <LogoutButton />
    </div>
  );
}

export default Username;
