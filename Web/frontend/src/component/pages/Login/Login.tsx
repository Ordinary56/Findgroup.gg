import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { apiService } from "../../../api/apiService";
import { useAuth } from "../../Auth_Context/AuthContext";
import styles from "./login.module.css";
import { ROUTES } from "../../../App";

const LoginPage: React.FC = () => {
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();
  const { login } = useAuth(); // AuthContext-ből a login függvény

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null); // Reset error state before attempting login
    const res = await apiService.login(username, password);
    if (res.status !== 200) {
      console.error("Login failed:", res);
      setError("Login failed! Please try again.");
      return;
    }
    login(); // Bejelentkezés állapot frissítése
    setError("There was an error during the login attempt");
    navigate(ROUTES.homepage.path); // Sikeres login után átirányítás
  };

  return (
    <div className={styles.loginContainer}>
      <form onSubmit={handleLogin} className={styles.loginForm}>
        <h1>Login to FindGroup</h1>
        <div>
          <label htmlFor="username">Username:</label>
          <input
            id="username"
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            className={styles.loginInput}
            placeholder="Enter your username"
          />
        </div>
        <div>
          <label htmlFor="password">Password:</label>
          <input
            id="password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className={styles.passwordInput}
            placeholder="Enter your password"
          />
        </div>
        <button type="submit" className={styles.LoginButton}>
          Login
        </button>
        <p>{error}</p>
        <div>
          Don't have an account?
          <Link to={ROUTES.register.path}> Register now!</Link>
        </div>
        {error && <p className={styles.errorMessage}>{error}</p>}
      </form>
    </div>
  );
};

export default LoginPage;
