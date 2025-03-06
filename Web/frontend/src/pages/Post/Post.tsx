import { useEffect, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { Post as PostModel } from "../../api/Models/Post";
import { apiService } from "../../api/apiService";
import { User } from "../../api/Models/User";
import styles from "./post.module.css";
import BackToHomeButton from "../../component/Back_To_Home_Button/Back_to_Home";
import { Button } from "@mui/material"; // MUI Button importálása

const Post = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const queryParams = new URLSearchParams(location.search);
  const [post, setPost] = useState<PostModel>();
  const [creator, setCreator] = useState<User>();
  const [isJoining, setIsJoining] = useState(false); // Csatlakozás állapota

  useEffect(() => {
    const fetchPost = async () => {
      try {
        const post = await apiService.getPost(parseInt(queryParams.get("id") as string));
        console.log(post);
        const creator = await apiService.getUser(post.userId);
        setPost(post);
        setCreator(creator);
      } catch (error) {
        console.error("Failed to fetch post or creator:", error);
      }
    };
    fetchPost();
  }, []);

  const handleJoinGroup = async () => {
    if (!post) return;
    setIsJoining(true);
    try {
      const userInfo = await apiService.getUserInfo();

      console.log(userInfo)
      const response = await apiService.joinGroup(post.group.id, userInfo.nameidentifier);
      console.log("Joined group:", response.data);
      navigate(`/group/${post.group.id}`);

    } catch (error) {
      console.error("Error joining group:", error);
    } finally {
      setIsJoining(false);
    }
  };
  const handleRevisit = async () => {
    if (!post) return;
    navigate(`/group/${post.group.id}`);
  };

  return (
    <>
      <BackToHomeButton />
      <Button
        variant="contained"
        color="primary"
        onClick={handleRevisit}>
        Go to this group
      </Button>
      {post ? (
        <div className={styles.container}>
          <h1>{post.title}</h1>
          <div className={styles.creatorAndLimit}>
            <h4>Created by {creator?.userName || "USER NOT FOUND"}</h4>
            <h4>Member Limit: {post.group?.memberLimit || "NULL"}</h4>
          </div>
          <h4>{post.group?.users.map((u, i) => (
            <Link to={`/profile/${u.id}`} >{u.userName}</Link>
          )) || "No members found in this group"}</h4>
          <div className={styles.Content}>
            <h2>{post.content}</h2>
          </div>
          <Button
            variant="contained"
            color="primary"
            onClick={handleJoinGroup}
            disabled={isJoining}
          >
            {isJoining ? "Joining..." : "Join Group"}
          </Button>


        </div>
      ) : (
        <div>error</div>
      )}
    </>
  );
};

export default Post;
