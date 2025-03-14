import axiosInstance from "./axiosInstance";
import { GroupDTO } from "./DTOs/GroupDTO";
import { PostDTO } from "./DTOs/PostDTO";
import { Category } from "./Models/Category";
import { Post } from "./Models/Post";
import { User } from "./Models/User";
import { Group } from "./Models/Group";
import { AxiosResponse } from "axios";
import { UserInfo } from "./Models/UserInfo";
import { Message } from "./Models/Message";
export const apiService = {

  login: async (username: string, password: string): Promise<AxiosResponse> => {
    const data: AxiosResponse = await axiosInstance.post("/Auth/login", { Username: username, Password: password });
    console.log(data);
    return data
  },

  logout: async (): Promise<void> => {
    try {
      await axiosInstance.post("/Auth/logout");

    } catch (error) {
      console.error("Logout error:", error);
    } finally {
      window.location.href = "/login"; // Átirányítás bejelentkezési oldalra
    }
  },

  register: async (username: string, password: string, email?: string): Promise<void> => {
    await axiosInstance.post("/Auth/register", { username, password, email });
  },

  getUser: async (id: string) => {
    const { data }: AxiosResponse<User> = await axiosInstance.get(`/User/${id}`);
    return data;
  },
  getUserInfo: async (): Promise<UserInfo> => {
    const { data }: AxiosResponse<UserInfo> = await axiosInstance.get("/User/me");
    return data;
  },

  getCategories: async (): Promise<Category[]> => {
    const { data } = await axiosInstance.get("/Category");
    return data;
  },

  //  Get all posts
  getPosts: async (): Promise<Post[]> => {
    const { data }: AxiosResponse<Post[]> = await axiosInstance.get("/Post");
    return data;
  },

  // Get a single post by ID
  getPost: async (id: number): Promise<Post> => {
    const { data }: AxiosResponse<Post> = await axiosInstance.get(`/Post/${id}`);
    return data;
  },

  // Create a new post
  //  NOTE: returns the post itself, not just the ID
  createPost: async (postDTO: PostDTO): Promise<Post> => {
    const { data } = await axiosInstance.post("/Post", postDTO);
    // remember: ID returned here;
    return data;
  },

  // Modify an existing post
  modifyPost: async (postDTO: PostDTO): Promise<void> => {
    await axiosInstance.patch(`/Post`, postDTO);
  },

  deletePost: async (id: number): Promise<AxiosResponse> => {
    const response = await axiosInstance.delete(`/Post/${id}`);
    return response
  },

  getGroups: async (): Promise<Group[]> => {
    const { data }: AxiosResponse = await axiosInstance.get("/Group");
    return data;
  },

  getGroupById: async (id: string): Promise<Group> => {
    const { data }: AxiosResponse = await axiosInstance.get(`Group/${id}`);
    return data;
  },


  // Create a new group associated with the post
  createGroup: async (GroupDTO: GroupDTO): Promise<Group> => {
    const { data } = await axiosInstance.post("/Group/create", GroupDTO);
    return data;
  },

  joinGroup: async (groupId: string, userId: string): Promise<AxiosResponse> => {
    const res = await axiosInstance.post("/Group/join?groupId=" + groupId + "&userId=" + userId)
    return res;
  },
  leaveGroup: async (groupId: string, userId: string): Promise<{ data: any, status: number }> => {
    const { data, status }: AxiosResponse<any, any> = await axiosInstance.post("/Group/leave?groupId=" + groupId + "&userId=" + userId);
    return { data, status }
  },
  sendMessage: async (message: Message) => {
    const { data } = await axiosInstance.post("/Message", message);
    return data;
  },
  modifyMessage: async (newMessage: Message) => {
    const { data } = await axiosInstance.patch("/Message", newMessage);
    return data;
  },
  deleteMessage: async (message: Message) => {
    const { data } = await axiosInstance.delete("/Message", message);
    return data;
  }
};
