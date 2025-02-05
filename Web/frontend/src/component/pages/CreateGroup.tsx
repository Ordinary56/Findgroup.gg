import styles from "./module.css/creategroup.module.css";
import BackToHomeButton from "../Back_To_Home_Button/Back_to_Home";

const CreateGroup = () => {
  return (
     <div className={styles.main}>
      <BackToHomeButton />
      <div className={styles.details_inputs}>
        <form>
          <input type="text" placeholder="Group Name" />
          <input type="text" placeholder="Group Description" />
          <select>
            <option value=""></option>
          </select>
          <select>
            <option value=""></option>
          </select>
        </form>
      </div>
      <div className={styles.very_basic_details}>
        <div className={styles.title_and_team_size}>
          <h1 className={styles.title}>a</h1>
          <h3 className={styles.creatorname}>b</h3>
          <span className={styles.team_size}>10</span>
        </div>
        <div className={styles.tags}>
          <div>asd</div>
        </div>
        <div className={styles.description}>masik</div>
      </div>
      <button className={styles.create_listing}>Create Listing</button>
    </div>
  )
}

export default CreateGroup