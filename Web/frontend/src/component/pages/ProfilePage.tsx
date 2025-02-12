import styles from "./module.css/profile.module.css";
import { useParams, Link } from "react-router-dom";
import { User } from "../../api/Models/User";
import { ROUTES } from "../../App";
import pfp from "../../../public/Thepic.png"

  
  type UserDetailsPageProps = {
    users: User[]; 
  }

const UserDetailsPage = ({users}:UserDetailsPageProps) => {
    const { id } = useParams<{ id: string }>();
    const user = users.find((u) => u.id === id);
  
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
    <div className="user-details">
      <h1>{user.userName}</h1>
      <img src={pfp} alt="User pfp"/>
    <Link to={ROUTES.aftercreate.path}>Back to previous page</Link>
    </div>
  );
};
export default UserDetailsPage;