import { useState } from "react";
import "./Login.css";
import IdentityService from "./services/IdentityService";

interface Props {
  setToken(loginResponseModel: any): void;
}

const Login = (props: Props) => {
  const { setToken } = props;
  const [email, setsetEmail] = useState<string>();
  const [password, setPassword] = useState<string>();

  const handleSubmit = async (e: any) => {
    e.preventDefault();
    const token = await IdentityService.login({
      email,
      password,
    });
    setToken(token);
  };

  return (
    <div className='login-wrapper'>
      <h1>Please Log In</h1>
      <form onSubmit={handleSubmit}>
        <label>
          <p>Username</p>
          <input type='text' onChange={(e) => setsetEmail(e.target.value)} />
        </label>
        <label>
          <p>Password</p>
          <input
            type='password'
            onChange={(e) => setPassword(e.target.value)}
          />
        </label>
        <div>
          <button type='submit'>Submit</button>
        </div>
      </form>
    </div>
  );
};

export default Login;
