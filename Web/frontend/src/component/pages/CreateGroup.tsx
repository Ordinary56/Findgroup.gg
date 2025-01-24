import styles from "./module.css/creategroup.module.css";
import BackToHomeButton from "../Back_To_Home_Button/Back_to_Home";

const CreateGroup = () => {
  return (
     <div>
      <BackToHomeButton />
      <div className={styles.details_inputs}>
        <form>
          <input type="text"/>
          <input type="text"/>
          <select>
            <option value=""></option>
          </select>
          <select>
            <option value=""></option>
          </select>
        </form>
      </div>
      <div>
        <div className={styles.very_basic_details}>
          <h3></h3>
          <h3></h3><span></span>
        </div>
        <div className={styles.tags}>
          <div></div>
        </div>
        <div className={styles.description}></div>
      </div>
      <button>Create Listing</button>
    </div>
  )
}

export default CreateGroup