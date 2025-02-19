import axiosInstance from "./axiosInstance";
import { GroupDTO } from "./DTOs/GroupDTO";
import { PostDTO } from "./DTOs/PostDTO";
import { Category } from "./Models/Category";
import { Post } from "./Models/Post";
import { User } from "./Models/User";
import { Group } from "./Models/Group";
import { AxiosResponse } from "axios";
import Cookies from "js-cookie";
export const apiService = {

  login: async (username: string, password: string): Promise<AxiosResponse> => {
    const  data : AxiosResponse  = await axiosInstance.post("/Auth/login", { Username: username, Password: password });
    console.log(data);
    return data
  },

  logout: async (): Promise<void> => {
    try {
      await axiosInstance.post("/Auth/logout");
      Cookies.remove("accessToken");
      Cookies.remove("refreshToken");
    } catch (error) {
      console.error("Logout error:", error);
    } finally {
      window.location.href = "/login"; // Átirányítás bejelentkezési oldalra
    }
  },

  register: async (username: string, password: string, email?: string): Promise<void> => {
    await axiosInstance.post("/Auth/register", { username, password, email });
  },
  getCategories: async (): Promise<Category[]> => {
    const { data } = await axiosInstance.get("/Category");
    return data;
  },

  //  Get all posts
  getPosts: async (): Promise<Post[]> => {
    const {data} : AxiosResponse<Post[]> = await axiosInstance.get("/Post");
    return data;
  },

  // Get a single post by ID
  getPost: async (id: number): Promise<Post> => {
    const {data} : AxiosResponse<Post>   = await axiosInstance.get(`/Post/${id}`);
    return data;
  },

  //  Create a new post
  createPost: async (postDTO: PostDTO): Promise<PostDTO> => {
    const { data } = await axiosInstance.post("/Post", postDTO);
    return data;
  },
  // Modify an existing post
  modifyPost: async (postDTO: PostDTO): Promise<void> => {
    await axiosInstance.patch(`/Post`, postDTO);
  },
  deletePost: async (id : number) : Promise<any> => {
    await axiosInstance.delete(`/Post/${id}`);
  },

  getGroups: async (): Promise<Group[]> => {
    const data: Group[] = await axiosInstance.get("/Group");
    return data;
  },

  getGroupById: async (id: string): Promise<Group> => {
    const group: Group = await axiosInstance.get(`Group/${id}`);
    return group;
  },


  // Create a new group associated with the post
  createGroup: async (GroupDTO: GroupDTO): Promise<any> => {
    const { data } = await axiosInstance.post("/Group/create", GroupDTO);
    return data;
  },

  joinGroup : async (groupId: string) : Promise<any> => {
    const res = await axiosInstance.post("/Group/join?")
    return res;
  },
};
