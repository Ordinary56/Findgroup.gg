import { useEffect, useState } from "react";
import { useLocation, useParams } from "react-router-dom"
import { Post as PostModel } from "../../api/Models/Post";
import { apiService } from "../../api/apiService";

const Post = () => {
    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);
    const [post, setPost] = useState<PostModel>();
    useEffect(()=> {
        const fetchPost = async() => {
            const post = await apiService.getPost(parseInt(queryParams.get("id") as string));
            setPost(post);
        }
        fetchPost();
    }, []);
    return (
        <>
        {post ? (
            <>
            <h1>{post.title}</h1>
            <h2>{post.content}</h2>

            </>
        ) : (
            <div>error</div>
        )}
        </>
    )
}

export default Post;