import { ChangeEvent, useState } from "react";
import { useNavigate } from "react-router-dom";
import "../stylesheets/Register.css";
import { useAuthContext } from "../contexts/AuthContext";

function Register() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [check, setCheck] = useState("");
  const [error, setError] = useState("");
  const { register } = useAuthContext(); 
  const navigate = useNavigate();

  async function registerCreds() {
    try {
      await register(username, password);
      navigate("/login");
    } catch (error) {
      setError("A network error has occured.");
      console.error(error);
    }
  }

  function handleSubmit(e: React.FormEvent<HTMLFormElement>){
    e.preventDefault();
    if (!checkMatching()) {
      return; 
    }

    registerCreds();
  }

  function checkMatching() {
    if (password == check ) {
      setError("");
      return true;
    }
    setError("Passwords do not match!");
    return false;
  }

  function handleUsername(e: ChangeEvent<HTMLInputElement>) { setUsername(e.target.value); }
  function handlePassword(e: ChangeEvent<HTMLInputElement>) { setPassword(e.target.value); }
  function handleCheck(e: ChangeEvent<HTMLInputElement>) { setCheck(e.target.value); }
  
  return (
    <div className="register">
      <form id="register-form" onSubmit={handleSubmit}>
        <div className="register-group">
          <input id="username" type="text" onChange={handleUsername} value={username} placeholder="Username" required />
        </div>
        <div className="register-group">
          <input id="password" type="password" onChange={handlePassword} value={password} placeholder="Password" required />
        </div>
        <div className="register-group">
          <input id="check" type="password" onChange={handleCheck} value={check} placeholder="Password" required />
        </div>
        <div className="register-group">
          <input id="register-btn" type="submit" value="Register" />
        </div>
      </form>
      <div className="error">
        { error && <div className="error-msg" style={{color : "red"}}>{error}</div>}
      </div>
    </div>
  );
}

export default Register;
