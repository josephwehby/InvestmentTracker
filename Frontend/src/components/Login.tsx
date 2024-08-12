import {ChangeEvent, useState} from "react";
import { useNavigate } from 'react-router-dom';
import "../stylesheets/Login.css";
import { useAuthContext } from "../contexts/AuthContext";

function Login() {
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [error, setError] = useState<string>("");
  const { login } = useAuthContext();
  const navigate = useNavigate();

  async function submitCreds() {
    try {
      await login(username, password);
      navigate("/portfolio");
    } catch (error) {
      console.error(error);
      setError("A network error has occured.");
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
        { error && <div className="error-msg" style={{color: 'red'}}>{error}</div> }
      </div>
    </div>
  );
}

export default Login;
