import { useParams } from "react-router-dom"
import styles from "./group.module.css";
import { useEffect, useState } from "react";
import { apiService } from "../../api/apiService";
import { Group as GroupModel } from "../../api/Models/Group";
import Message from "../../component/Message/Message";
import { Message as MessageModel } from "../../api/Models/Message";
import { UserInfo } from "../../api/Models/UserInfo";
import { Link, useNavigate } from "react-router-dom";
const Group = () => {
  let { groupId } = useParams();
  const navigate = useNavigate();
  const [group, setGroup] = useState<GroupModel | undefined>();
  const [inputMessage, setInputMessage] = useState<string>("")
  const [userInfo, setUserInfo] = useState<UserInfo>();
  const [isSidebarOpen, setIsSidebarOpen] = useState(false);

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
  const toggleSidebar = () => {
    setIsSidebarOpen(prevState => !prevState);
  };
  return (
    <div className={styles.container}>

      {/* Sidebar dinamikus osztállyal */}
      <div className={`${styles.sidebar} ${isSidebarOpen ? styles.open : ""}`}>

        <button onClick={() => navigate(-1)} className={styles.backButton}>
          Back To Home
        </button>
        <h2>Members</h2>
        <div className={styles.members}>
          {/* Ha a group.users nincs definiálva, akkor ne csináljon hibát */}
          {group?.users?.map(user => (
            <Link key={user.id} to={`/profile/${user.id}`}>
              <h3>{user.userName}</h3>
            </Link>
          ))}
        </div>
        <button className={styles.toggle_button} onClick={toggleSidebar}>
          {isSidebarOpen ? (
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="white">
              <path d="M15 18l-6-6 6-6" stroke="white" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
            </svg>
          ) : (
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="white">
              <path d="M9 6l6 6-6 6" stroke="white" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
            </svg>
          )}
        </button>
      </div>

      <div className={styles.messagecontainer}>
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
    </div>
  )
}

export default Group
