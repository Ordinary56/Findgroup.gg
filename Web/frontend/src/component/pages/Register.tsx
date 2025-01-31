import React, { useState } from "react";
import { apiService } from "../../api/apiService";
import styles from "./module.css/register.module.css";
import { Link } from "react-router-dom";
import { ROUTES } from "../../App";

const RegisterPage: React.FC = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [email, setEmail] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setSuccess(null);

    try {
      await apiService.register(username, password, email);
      setSuccess("Sikeres regisztráció! Most már bejelentkezhetsz.");
    } catch (err) {
      console.error(err);
      setError((err as Error).message);
    }
  };

  return (
    <div className={styles.registerContainer}>
    <form onSubmit={handleRegister} className={styles.registerForm}>
    <h1>Register to FindGroup</h1>
      <div>
        <label>UserName:</label>
        <input className={styles.registerInput}
          type="text"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          required
          placeholder="Enter a username"
        />
      </div>
      <div>
        <label>Email:</label>
        <input className={styles.registerEmail}
          type="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
           placeholder="Enter an email"
        />
      </div>
      <div>
        <label>Password:</label>
        <input className={styles.registerPassword}
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
           placeholder="Enter a password"
        />
      </div>
      <button type="submit" className={styles.registerButton}>Register</button>
      <div>Already have an account?
      <Link to={ROUTES.login.path}> Log in!</Link>
      </div> 
      {success && <p style={{ color: "green" }}>{success}</p>}
      {error && <p style={{ color: "red" }}>{error}</p>}
    </form></div>
  );
};

export default RegisterPage;
