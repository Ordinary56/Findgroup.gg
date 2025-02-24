import styles from "./creategroup.module.css";
import BackToHomeButton from "../../Back_To_Home_Button/Back_to_Home";
import { Link } from "react-router-dom";
import { ROUTES } from "../../../App";
import { useState } from "react";

const CreateGroup = () => {
  const [postName, setPostName] = useState<string>("User's game");
  const [groupName, setGroupName] = useState("");

  return (
    <>
      <div className={styles.Back_to_Home}>
        <BackToHomeButton />
      </div>
      <div className={styles.main}>
        {/* Az alapvető adatok és az inputok egy konténerbe kerülnek */}
        <div className={styles.very_basic_details}>
          <div className={styles.details_inputs}>
            <form>
              <input
                type="text"
                name="postName"
                id="postName"
                placeholder="Post's name..."
                onInput={(e) =>
                  setPostName((e.target as HTMLInputElement).value)
                }
              />
              <input
                type="text"
                placeholder="Group Name"
                required
                value={groupName}
                onInput={(e) =>
                  setGroupName((e.target as HTMLInputElement).value)
                }
              />
              <input type="text" placeholder="Group Description" />
              <select>
                <option value="">Select Option</option>
              </select>
              <select>
                <option value="">Select Option</option>
              </select>
            </form>
          </div>
        </div>
        <div className={styles.preview}>
          <div className={styles.title_wrapper}>
            <h1 className={styles.title}>{postName}</h1>
          </div>

          <div className={styles.title_and_team_size}>
            <h3 className={styles.creatorname}>User</h3>
            <span className={styles.team_size}>Team size: 20</span>
          </div>

          <div className={styles.tags}>
            <div>Competitive</div>
          </div>
          <textarea className={styles.description}>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras eu est
            eros. Suspendisse vitae tortor id sem tempor rutrum. In tempus ante
            vel sapien ornare maximus. Nam varius est risus, a ullamcorper
            tortor consectetur ac. Phasellus sit amet efficitur augue. Mauris
            ornare arcu vel nisl commodo iaculis. Pellentesque congue justo nec
            tellus varius commodo. Vestibulum ac volutpat quam. Sed quis purus
            auctor, scelerisque tortor vel, fermentum ex. Aenean eget turpis
            finibus, scelerisque nibh vel, pulvinar est. Ut rutrum egestas
            venenatis. Nullam eu interdum enim, ac aliquet est. In auctor
            efficitur commodo. Suspendisse potenti. Ut ante orci, dictum in
            mollis vel.
          </textarea>
        </div>

  

        <button className={styles.create_listing}>
          {" "}
          <Link to={ROUTES.aftercreate.path}>Create Listing</Link>
        </button>
      </div>
    </>

  );
};

export default CreateGroup;
