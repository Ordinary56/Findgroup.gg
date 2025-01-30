import axiosInstance from "./axiosInstance";
type PostDTO = {
  id?: number; // Optional for creation
  title: string;
  content: string;
  // Add other fields as necessary
};
type Category = {
  id: number;
  name: string;
  description: string;
};
type ApiUser = {
  id: string;
  userName: string;
  email: string;
};
export const apiService = {
  // ✅ Token kezelés
  setToken: (token: string) => localStorage.setItem("accessToken", token),
  getToken: (): string | null => localStorage.getItem("accessToken"),
  setRefreshToken: (token: string) => localStorage.setItem("refreshToken", token),
  getRefreshToken: (): string | null => localStorage.getItem("refreshToken"),
  clearTokens: () => {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
  },

  // ✅ Bejelentkezés
  login: async (username: string, password: string): Promise<void> => {
    const { data } = await axiosInstance.post("/Auth/login", { username, password });
    apiService.setToken(data.token);
    apiService.setRefreshToken(data.refreshToken);
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

  // ✅ Felhasználói profil lekérése (JAVÍTVA: nincs szóköz a névben)
  getUserProfile: async (): Promise<ApiUser> => { // Szóköz eltávolítva
    const { data } = await axiosInstance.get("/Auth/profile");
    return data;
  },

  /*
  loginWithGoogle: async (googleToken: string): Promise<void> => {
    await axiosInstance.post("/Auth/google", { token: googleToken });
  },
  */
   // ✅ Get all posts
   getPosts: async (): Promise<PostDTO[]> => {
    const { data } = await axiosInstance.get("/Post");
    return data;
  },

  // ✅ Get a single post by ID
  getPost: async (id: number): Promise<PostDTO> => {
    const { data } = await axiosInstance.get(`/Post/${id}`);
    return data;
  },

  // ✅ Create a new post
  createPost: async (postDTO: PostDTO): Promise<PostDTO> => {
    const { data } = await axiosInstance.post("/Post", postDTO);
    return data;
  },

  // ✅ Modify an existing post
  modifyPost: async (postDTO: PostDTO): Promise<void> => {
    await axiosInstance.patch(`/Post`, postDTO);
  },
};
