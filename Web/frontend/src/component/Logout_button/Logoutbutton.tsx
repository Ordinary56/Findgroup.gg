import React, { useState } from "react";
import styles from "logoutbutton.module.css"

const LogoutButton: React.FC = () => {
  const [loading, setLoading] = useState<boolean>(false);
  const [message, setMessage] = useState<string>("");

  const handleLogout = async () => {
    setLoading(true);
    setMessage("");

    try {
      const response = await fetch("/api/logout", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include", // Ha sütiket is kezel az API
      });

      if (response.ok) {
        setMessage("Logout successful.");
      } else if (response.status === 401) {
        setMessage("You are not logged in.");
      } else {
        const errorData = await response.json();
        setMessage(
          `An error occurred during logout: ${errorData.message || "Unknown error"}`
        );
      }
    } catch (error) {
      setMessage(`An error occurred: ${(error as Error).message}`);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <button
        onClick={handleLogout}
        disabled={loading}
        className={styles.buttonstyle}
      >
        {loading ? "Logging out..." : "Logout"}
      </button>
      {message && (
        <p
          style={{
            marginTop: "10px",
            color: message.includes("successful") ? "green" : "red",
          }}
        >
          {message}
        </p>
      )}
    </div>
  );
};

export default LogoutButton;
