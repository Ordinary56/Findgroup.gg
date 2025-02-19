import { useEffect, useState } from "react";
import { apiService } from "../api/apiService";
import { Post } from "../api/Models/Post";
import { AxiosError } from "axios";


const UsePosts = () => {
    const [posts, setPosts] = useState<Post[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<AxiosError | undefined>();
    useEffect(() => {
        const fetchPosts = async () => {
            try {
                const res: Post[] = await apiService.getPosts();
                setPosts(res);
                setLoading(false);
            }
            catch (error) {
                setError(error as AxiosError);
                setLoading(false);
            }
        }
        fetchPosts();
    }, []);
    return {posts, loading, error}
}
export default UsePosts;