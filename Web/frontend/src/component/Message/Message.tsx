import styles from "./Message.module.css";
import { Message as MessageModel } from "../../api/Models/Message"
import { useEffect, useState } from "react";
import { User } from "../../api/Models/User";
import { apiService } from "../../api/apiService";
type MessageProps = {
	message: MessageModel
}
const Message = ({ message }: MessageProps) => {
	const [user, setUser] = useState<User>();
	useEffect(() => {
		const fetchUser = async () => {
			try {
				const res = await apiService.getUser(message.userId);
				setUser(res)
			}
			catch (err) {
				console.error(err);
			}
		};
		fetchUser();
	})
	return (
		<div className={styles.container}>
			<img src="/Thepic.png" alt={user?.userName} />
			<div className={styles.infoContainer}>
				<h2>{user?.userName}</h2>
				<p>{message.content}</p>
			</div>
		</div>
	)
}

export default Message;
