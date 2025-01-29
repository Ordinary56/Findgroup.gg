import React from "react";
import { useNavigate } from "react-router-dom";
import styles from "./module.css/landingPage.module.css";

const LandingPage: React.FC = () => {
  const navigate = useNavigate();

  const handleLoginClick = () => {
    navigate("/login"); // Navigálás a login oldalra
  };
  const handleRegisterClick = () => {
    navigate("/register"); // Navigálás a login oldalra
  };

  return (
    <div className={styles.landingPage}>
      <h1>Welcome to FindGroup!</h1>
      <p>Discover and join groups for your favorite games!</p>
      <div className={styles.buttonContainer}><button className={styles.loginButton} onClick={handleLoginClick}>
        Login
      </button>
            <span>Or</span>
              <button className={styles.registerButton} onClick={handleRegisterClick}>
        Sign Up
      </button></div>
    </div>
  );
};

export default LandingPage;
