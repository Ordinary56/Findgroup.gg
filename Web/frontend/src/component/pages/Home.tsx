import { useState } from "react"
import styles from "./module.css/home.module.css"

const Home = () => {
  const [activestyle, setActivestyle] = useState()
  
  return (
    <>
    <div className={styles.gamechooser}>
        <button className={styles.game}></button>
    </div>
    </>
  )
}

export default Home