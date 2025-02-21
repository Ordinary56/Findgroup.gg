import { useEffect, useState } from "react";
import { useLocation, useParams } from "react-router-dom"
import { Post as PostModel } from "../../api/Models/Post";
import { apiService } from "../../api/apiService";
import { User } from "../../api/Models/User";

const Post = () => {
    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);
    const [post, setPost] = useState<PostModel>();
    const [creator, setCreator] = useState<User>();
    useEffect(()=> {
        const fetchPost = async() => {
            const post = await apiService.getPost(parseInt(queryParams.get("id") as string));
            const creator = await apiService.getUser(post.userId);
            setPost(post);
            setCreator(creator);
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