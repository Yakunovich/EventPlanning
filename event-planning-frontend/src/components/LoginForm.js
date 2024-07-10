import React, { useState, useContext } from "react";
import accountService from "../services/accountService";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../AuthContext";

const LoginForm = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState(null);
  const navigate = useNavigate();
  const { login } = useContext(AuthContext);

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const data = await accountService.authenticate(email, password);
      const token = data.token;
      login(token);
      setError(null);
      navigate("/");
    } catch (error) {
      console.error("Error logging in", error);
      setError(error.response.data);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <label>
        Email:
        <input
          type="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
      </label>
      <label>
        Password:
        <input
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
      </label>
      {error && <p>{error}</p>}
      <button type="submit">Login</button>
    </form>
  );
};

export default LoginForm;
