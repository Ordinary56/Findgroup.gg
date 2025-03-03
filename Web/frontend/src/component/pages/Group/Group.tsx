import {  useSearchParams } from "react-router-dom"
import styles from "./group.module.css";
import BackToHomeButton from "../../Back_To_Home_Button/Back_to_Home";

const Group = () => {
  const params = useSearchParams();
  

  return (
    <>
  <BackToHomeButton />
  </>
  )
}

export default Group