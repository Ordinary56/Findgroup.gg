import styles from "./creategroup.module.css";
import BackToHomeButton from "../../Back_To_Home_Button/Back_to_Home";
import { Link } from "react-router-dom";
import { ROUTES } from "../../../App";
import {  useState } from "react";
import { User } from "../../../api/Models/User";
import { apiService } from "../../../api/apiService";

//TODO : Rework this component

const CreateGroup = () => {
  const [postName, setPostName] = useState<string>("");
  const [groupDesc, setgroupDesc] = useState<string>("");
  

  return (
    <>
      <div className={styles.Back_to_Home}>
        <BackToHomeButton />
      </div>
      <div className={styles.main}>
        <div className={styles.very_basic_details}>
          <div className={styles.details_inputs}>
            <form>
              <input
                type="text"
                name="postName"
                
                placeholder="Group's name..."
                onInput={(e) =>
                  setPostName((e.target as HTMLInputElement).value)
                }/>

              <input 
              type="text"
              placeholder="Group Description"
              name="groupDesc"
              
              onInput={(e) =>
                  setgroupDesc((e.target as HTMLInputElement).value)
                }/>

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
            <h3 className={styles.creatorname}></h3>
            <span className={styles.team_size}>Team size: </span>
          </div>
        
          <p className={styles.description}>{groupDesc}</p>
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
