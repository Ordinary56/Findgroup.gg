import { useState } from "react";
import styles from "./module.css/home.module.css";

const Home = () => {
  const [isHighlighted, setIsHighlighted] = useState(false);

  const handleDivClick = () => {
    setIsHighlighted((prev) => !prev);
  };

  return (
    <>
      <div className={styles.gamechooser}>
        <div
          onClick={handleDivClick}
          className={`${styles.gamechooserItem} ${isHighlighted ? styles.Highlighted : ""}`}
        >
          asd
        </div>
      </div>
    </>
  );
};

export default Home;
