import {ChangeEvent, useState} from "react";
import "../stylesheets/Login.css";

function Login() {
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [error, setError] = useState<string>("");

  async function submitCreds() {
    try {
      const response = await fetch("https://localhost:7274/login", {
        method: "POST",
        headers: { 
          "Accept":"application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          "username": username,
          "password": password
        })
      });

      if (!response.ok) {
       setError("Invalid login credentials."); 
       return;
      }
      setError("");
      const token = await response.json();
      console.log(token);
    } catch (error) {
      console.error("[!] Error logging in: " + error);
    }
  }

  function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    submitCreds();
  }

  function handleUsername(e: ChangeEvent<HTMLInputElement>) { setUsername(e.target.value); }
  function handlePassword(e: ChangeEvent<HTMLInputElement>) { setPassword(e.target.value); }
  
  return(
    <div className="login">
      <form onSubmit={handleSubmit}>
        <div className="login-group">
          <input id="username" type="text" placeholder="Username" value={username} onChange={handleUsername} required />
        </div>
        <div className="login-group">
          <input id="password" type="password" placeholder="Password" value={password} onChange={handlePassword} required />
        </div>
        <div className="login-group">
          <input id="login-btn" type="submit" value="Login" />
        </div>
      </form>
      <div className="error">
        { error && <div className="error-msg">{error}</div> }
      </div>
    </div>
  );
}

export default Login;