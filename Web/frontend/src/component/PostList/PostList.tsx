import styles from "./PostList.module.css"

import { Link } from "react-router-dom";
import UsePosts from "../../hooks/usePosts";
import Group from "../../pages/Group/Group";
// TODO: Work on this component further
interface PostListProps {
    selectedGame: string;
}

const PostList: React.FC<PostListProps> = ({ selectedGame }) => {
    const { posts, loading, error } = UsePosts(selectedGame);
    console.log(posts);
    if (loading) return <div>Loading...</div>
    if (error) return <div>Error</div>
    return (
        <ul className={styles["container"]}>
            {posts.filter(post => post.category.categoryName == selectedGame).map(post => (
                <Link to={{
                    pathname: "/post",
                    search: `?id=${post.id}`
                }} key={post.id}>
                    <li key={post.id} className={styles["container-item"]}>

                        <h1>{post.title}</h1>
                        <h3>{new Date(post.createdDate).toLocaleDateString()}</h3>
                    </li>
                </Link>
            ))}
        </ul>
    );
}

export default PostList;
