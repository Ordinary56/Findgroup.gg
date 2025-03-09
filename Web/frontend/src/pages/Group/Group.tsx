import { useParams } from "react-router-dom"
import styles from "./group.module.css";
import BackToHomeButton from "../../component/Back_To_Home_Button/Back_to_Home";
import { useEffect, useState } from "react";
import { apiService } from "../../api/apiService";
import { Group as GroupModel } from "../../api/Models/Group";
import Message from "../../component/Message/Message";
import { Message as MessageModel } from "../../api/Models/Message";
import { UserInfo } from "../../api/Models/UserInfo";

const Group = () => {
  let { groupId } = useParams();
  const [group, setGroup] = useState<GroupModel | undefined>();
  const [inputMessage, setInputMessage] = useState<string>("")
  const [userInfo, setUserInfo] = useState<UserInfo>();
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
    // NOTE: This should work better if it was in a context
    // hmmm....
    // TODO: make this in a context rather than needlessly stealing the bandwith from backend
    const fetchUserInfo = async () => {
      try {
        const userInfo = await apiService.getUserInfo();
        setUserInfo(userInfo);
      }
      catch (err) {
        console.error("Something went wrong when fetching user info: ", err)
      }
    }
    fetchGroup();
    fetchUserInfo();
  }, []);

  const sendMessage = async () => {
    const message: MessageModel = {
      content: inputMessage,
      groupId: group!.id,
      userId: userInfo!.nameidentifier
    }

  }
  return (
    <div className={styles.container}>

      <BackToHomeButton />
      <div className={styles.members}>
        {group?.users.map(user => (
          <h2>{user.userName}</h2>
        ))}
      </div>
      <div className={styles.messagesWrapper}>
        {group?.messages.map(message => (
          <Message key={message.id} message={message} />
        ))}
      </div>
      <div className={styles.inputWrapper}>
        <input type="text" value={inputMessage}
          onInput={e => setInputMessage(e.target.value)}
          placeholder="type your message here" />
      </div>


    </div>
  )
}

export default Group
