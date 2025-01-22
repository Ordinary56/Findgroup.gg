import React, { useState } from "react";
import { apiService } from "../../api/apiService";
import styles from "./login.module.css";

const LoginPage: React.FC = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await apiService.login(username, password);
      alert("Sikeres bejelentkezés!");
    } catch (err) {
      console.error(err);
      setError("Bejelentkezés sikertelen!");
    }
  };

  return (
    <div className={styles.loginContainer}>
    <form onSubmit={handleLogin} className={styles.loginForm}>
      <h1>Login to FindGroup</h1>
      <div>
        <label>Username:</label>
        <input className={styles.loginInput}
          type="text"
          value={username}
          onChange={(e) => setUsername(e.target.value) }
        />
      </div>
      <div>
        <label>Password:</label>
        <input className={styles.passwordInput}
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </div>
      <button type="submit" className={styles.loginButton}>Login</button>
      {error && <p style={{ color: "red" }}>{error}</p>}
    </form></div>
  );
};

export default LoginPage;
