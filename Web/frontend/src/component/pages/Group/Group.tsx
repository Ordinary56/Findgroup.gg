import { useParams } from "react-router-dom";
import styles from "./group.module.css";
import { Group as GroupModel } from "../../../api/Models/Group";
import BackToHomeButton from "../../Back_To_Home_Button/Back_to_Home";
import { useState, useEffect } from "react";
import { apiService } from "../../../api/apiService"; 

const Group = () => {
  const { groupId } = useParams<{ groupId: string }>();

  console.log("Extracted groupId from URL:", groupId); 

  const [group, setGroup] = useState<GroupModel | null>(null);

  useEffect(() => {
    const fetchGroupData = async () => {
      if (!groupId) {
        console.error("Group ID is missing in the URL");
        return; 
      }

      try {
        const fetchedGroup = await apiService.getGroupById(groupId);
        setGroup(fetchedGroup);
      } catch (error) {
        console.error("Failed to fetch group data", error);
      }
    };

    fetchGroupData();
  }, [groupId]); 

  return (
    <>
      <BackToHomeButton />
      {group ? (
        <div className={styles.container}>
          <h4>
            {group.users && group.users.length > 0
              ? group.users.map((user) => user.userName).join(", ")
              : "No members found in this group"}
          </h4>
        </div>
      ) : (
        <div>Loading...</div>
      )}
      <div className={styles.chatContaier}>
        <div className={styles.messagesContainer}></div>
        <input type="text"  className={styles.chatInput}/>
      </div>
    </>
  );
};

export default Group;
