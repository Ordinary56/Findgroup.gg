import {use} from "react";
import { Post } from "../../api/Models/Post";
type PostListProps = {
    postPromise : Promise<Post[]>
}
// TODO: Work on this component further
const PostList = ({postPromise} : PostListProps) => {
    const posts = use(postPromise)
    return (
        <ul>
            {posts.map(post => (
                <li key={post.id}>
                    <h1>{post.title}</h1>
                    
                </li>
            ))}
        </ul>
    );
}

export default PostList;
