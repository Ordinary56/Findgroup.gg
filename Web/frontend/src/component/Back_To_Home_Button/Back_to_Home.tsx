import { Link } from "react-router-dom"
import { ROUTES } from "../../App"
import styles from "./backtohome.module.css"

const BackToHomeButton = () => {
  return (
      <div className={styles.button}>        
      <Link to={ROUTES.homepage.path}>Back to Home</Link>
      </div>     
  )
}

export default BackToHomeButton