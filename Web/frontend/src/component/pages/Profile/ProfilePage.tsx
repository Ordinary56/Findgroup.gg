import { useState } from "react";
import styles from "./profile.module.css";
import { useParams, Link } from "react-router-dom";
import { User } from "../../../api/Models/User";
import { ROUTES } from "../../../App";
import pfp from "../../../public/Thepic.png";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";
import ToggleButton from "@mui/material/ToggleButton";

type UserDetailsPageProps = {
  users: User[];
};

const UserDetailsPage = ({ users }: UserDetailsPageProps) => {
  const { id } = useParams<{ id: string }>();
  const user = users.find((u) => u.id === id);

  const [view, setView] = useState<"friends" | "connected">("friends");

  const handleViewChange = (_event: React.MouseEvent<HTMLElement>, newView: "friends" | "connected" | null) => {
    if (newView !== null) {
      setView(newView);
    }
  };

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
      <Link to={ROUTES.aftercreate.path}>Back to previous page</Link>
      <div>
        <div className={styles.user_image_and_name}>
          <h1>{user.userName}</h1>
          <img src={pfp} alt="User pfp" />
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

export default UserDetailsPage;
