import { Link } from "react-router-dom"
import { ROUTES } from "../../App"
import styles from "./creategruoupbutton.module.css"

const CreateButton = () => {
  return (
      <div className={styles.button}>        
      <Link to={ROUTES.create.path}> Create a Group</Link>
      </div>     
  )
}

export default CreateButton