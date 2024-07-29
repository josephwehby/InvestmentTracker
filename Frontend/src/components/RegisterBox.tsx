import { ChangeEvent, useState } from "react";
import "../stylesheets/Register.css";

function Register() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [check, setCheck] = useState("");
  
  async function registerCreds() {
    // make fetch api call to add new user
  }

  function handleSubmit(e: React.FormEvent<HTMLFormElement>){
    e.preventDefault();
    registerCreds();
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
    </div>
  );
}

export default Register;