import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { apiService } from "../../api/apiService";
import styles from "./module.css/login.module.css";

const LoginPage: React.FC = () => {
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null); // Reset error state before attempting login
    try {
      await apiService.login(username, password);
      alert("Sikeres bejelentkezés!");
      navigate("/"); // Redirect to the homepage on successful login
    } catch (err) {
      console.error("Bejelentkezés sikertelen:", err);
      setError("Bejelentkezés sikertelen! Kérjük, ellenőrizd az adataidat.");
    }
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
        {error && <p className={styles.errorMessage}>{error}</p>}
      </form>
    </div>
  );
};

export default LoginPage;
