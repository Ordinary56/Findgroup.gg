import { useEffect, useState } from "react";
import styles from "./profile.module.css";
import { useParams, useNavigate } from "react-router-dom";
import { User } from "../../api/Models/User";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";
import ToggleButton from "@mui/material/ToggleButton";
import { apiService } from "../../api/apiService";

const Profile = () => {
  const { id } = useParams<{ id: string }>();
  const [user, setUser] = useState<User>();
  const [view, setView] = useState<"friends" | "connected">("friends");
  const navigate = useNavigate(); // Hook for navigation

  const handleViewChange = (_event: React.MouseEvent<HTMLElement>, newView: "friends" | "connected" | null) => {
    if (newView !== null) {
      setView(newView);
    }
  };

  useEffect(() => {
    const fetchUser = async () => {
      const user = await apiService.getUser(id);
      setUser(user);
    };
    fetchUser();
  }, [id]);

  if (!user) {
    return <p>User cannot be found.</p>;
  }

  return (
    <>
      <div>
        <div className={styles["profile-container"]}>
          <img src="/Thepic.png" alt={`${user.userName}'s Profile Picture`} />
          <h1>{user.userName}</h1>
        </div>

        {/* Back button */}
        <button onClick={() => navigate(-1)} className={styles.backButton}>
          Back
        </button>

        {/* Toggle Button Group */}
        <ToggleButtonGroup
          value={view}
          exclusive
          onChange={handleViewChange}
          aria-label="view toggle"
        >
          <ToggleButton value="friends">Friends</ToggleButton>
          <ToggleButton value="connected">Connected Services</ToggleButton>
        </ToggleButtonGroup>

        {/* Conditionally Render Divs */}
        {view === "friends" ? (
          <div className={styles.user_friends}>
            {/* Content for User Friends */}
            <p>Friends List</p>
          </div>
        ) : (
          <div className={styles.user_connected}>
            {/* Content for User Connected */}
            <p>Connected Users</p>
          </div>
        )}
      </div>
    </>
  );
};

export default Profile;
