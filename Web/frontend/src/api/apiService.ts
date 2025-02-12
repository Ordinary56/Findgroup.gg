import { useState } from "react";
import axiosInstance from "./axiosInstance";
import { GroupDTO } from "./DTOs/GroupDTO";
import { PostDTO } from "./DTOs/PostDTO";
import { Category } from "./Models/Category";
import { Post } from "./Models/Post";
import { User } from "./Models/User";
import { tokenService } from "./tokenService";
export const apiService = {
  // ✅ Token kezelés
  clearTokens: () => {
      tokenService.setToken("");
  },

  // ✅ Bejelentkezés
  login: async (username: string, password: string): Promise<void> => {
    const { data } = await axiosInstance.post("/Auth/login", { Username:  username, Password: password });
    tokenService.setToken(data.token);
  },

  // ✅ Kijelentkezés (Logout)
  logout: async (): Promise<void> => {
    try {
      await axiosInstance.post("/Auth/logout");
    } catch (error) {
      console.error("Logout error:", error);
    } finally {
      apiService.clearTokens();
      window.location.href = "/login"; // Átirányítás bejelentkezési oldalra
    }
  },

  // ✅ Regisztráció
  register: async (username: string, password: string, email?: string): Promise<void> => {
    await axiosInstance.post("/Auth/register", { username, password, email });
  },
  // ✅ Kategóriák lekérése
  getCategories: async (): Promise<Category[]> => {
    const { data } = await axiosInstance.get("/Category");
    return data;
  },

  // ✅ Get all posts
   getPosts: async (): Promise<Post> => {
    const { data } = await axiosInstance.get("/Post");
    return data;
  },

  // ✅ Get a single post by ID
  getPost: async (id: number): Promise<Post> => {
    const { data } = await axiosInstance.get(`/Post/${id}`);
    return data;
  },

  // ✅ Create a new post
  createPost: async (postDTO: PostDTO): Promise<PostDTO> => {
    const { data } = await axiosInstance.post("/Post", postDTO);
    return data;
  },

  // Create a new group associated with the post
  createGroup : async(GroupDTO: GroupDTO) : Promise<any> => {
      const { data } = await axiosInstance.post("/Group/create", GroupDTO);
      return data;
  },

  // ✅ Modify an existing post
  modifyPost: async (postDTO: PostDTO): Promise<void> => {
    await axiosInstance.patch(`/Post`, postDTO);
  },
};
