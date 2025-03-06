import { useParams } from "react-router-dom"
import styles from "./group.module.css";
import BackToHomeButton from "../../component/Back_To_Home_Button/Back_to_Home";
import { useEffect, useState } from "react";
import { apiService } from "../../api/apiService";
import { Group as GroupModel } from "../../api/Models/Group";

const Group = () => {
  let { groupId } = useParams();
  const [group, setGroup] = useState<GroupModel | undefined>();
  useEffect(() => {
    const fetchGroup = async () => {
      try {
        const res = await apiService.getGroupById(groupId as string);
        console.log(res);
        setGroup(res);
      }
      catch (err) {
        console.error("Failed to fetch group for the following reason: ", err);
      }
    };
    fetchGroup();
  }, []);
  return (
    <>
      <BackToHomeButton />


    </>
  )
}

export default Group
