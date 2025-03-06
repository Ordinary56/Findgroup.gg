import { useEffect, useState } from "react";
import styles from "./profile.module.css";
import { useParams } from "react-router-dom";
import { User } from "../../api/Models/User";
import pfp from "../../../public/Thepic.png";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";
import ToggleButton from "@mui/material/ToggleButton";
import { apiService } from "../../api/apiService";

const Profile = () => {
  const { id } = useParams<{ id: string }>();
  const [user, setUser] = useState<User>();
  const [view, setView] = useState<"friends" | "connected">("friends");

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
  }, []);

  if (!user) {
    return <p>User cannot be found.</p>;
  }

  //TO THE PAGE THAT WILL OPEN THIS
  /*
   onClick={(e) => {
      e.preventDefault(); // Prevent React Router from handling it
      window.open(ROUTES.aftercreate.path, "_blank");
    }}
  */

  return (
    <>
      <div>
        <div className={styles["profile-container"]}>
          <img src={pfp} alt="User pfp" />
          <h1>{user.userName}</h1>
        </div>

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
