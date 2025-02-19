
import { Link } from "react-router-dom";
import { Post } from "../../api/Models/Post";
import UsePosts from "../../hooks/usePosts";
// TODO: Work on this component further
const PostList = () => {
    const {posts, loading, error} = UsePosts();
    console.log(posts);
    if(loading) return <div>Loading...</div>
    if(error) return <div>Error</div>
    return (
        <ul>
            {posts.map(post => (
                <li key={post.id}>
                    <Link to={{
                        pathname : "/post",
                        search : `?id=${post.id}`
                    }}>
                    <h1>{post.title}</h1>
                    </Link>
                </li>
            ))}
        </ul>
    );
}

export default PostList;
