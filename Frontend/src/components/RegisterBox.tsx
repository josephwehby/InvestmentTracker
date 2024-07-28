import { ChangeEvent, useState } from "react";

function Register() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [check, setCheck] = useState("");
  
  async function registerCreds() {

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
      <form onSubmit={handleSubmit}>
        <div className="register-group">
          <input id="username" type="text" onChange={handleUsername} value={username} required />
        </div>
        <div className="register-group">
          <input id="password" type="text" onChange={handlePassword} value={password} required />
        </div>
        <div className="register-group">
          <input id="check" type="text" onChange={handleCheck} value={check} required />
        </div>
        <div className="register-group">
          <input id="register-btn" type="submit" value="Register" />
        </div>
      </form>
    </div>
  );
}

export default Register;