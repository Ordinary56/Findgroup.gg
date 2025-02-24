import styles from "./PostList.module.css"
import { Link } from "react-router-dom";
import UsePosts from "../../hooks/usePosts";
// TODO: Work on this component further
interface PostListProps {
    selectedGame: string;
  }
  
  const PostList: React.FC<PostListProps> = ({ selectedGame }) => {
    const {posts, loading, error} = UsePosts();
    console.log(posts);
    if(loading) return <div>Loading...</div>
    if(error) return <div>Error</div>
    return (
        <ul className={styles["container"]}>
            {posts.map(post => (
                <li key={post.id} className={styles["container-item"]}>
                    <Link to={{
                        pathname : "/post",
                        search : `?id=${post.id}`
                    }}>
                    <h1>{post.title}</h1>
                    </Link>
                    <h3>{new Date(post.createdDate).toLocaleDateString()}</h3>
                </li>
            ))}
        </ul>
    );
}

export default PostList;
