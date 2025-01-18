import React, { useState } from "react";
import { apiService } from "../../api/apiService";

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
    <form onSubmit={handleLogin}>
      <div>
        <label>Felhasználónév:</label>
        <input
          type="text"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
      </div>
      <div>
        <label>Jelszó:</label>
        <input
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </div>
      <button type="submit">Bejelentkezés</button>
      {error && <p style={{ color: "red" }}>{error}</p>}
    </form>
  );
};

export default LoginPage;
