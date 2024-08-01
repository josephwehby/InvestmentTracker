import "../stylesheets/Username.css";

function Username() {
  const name = localStorage.getItem("username") || "Default";
  return (
    <div className="username-display">
      <p>Current User:&ensp; <span style={{color: "green"}}>{name}</span></p>
    </div>
  );
}

export default Username;