import axios from "axios";
import { apiService } from "./apiService";
import { tokenService } from "./tokenService";
import Cookies from "js-cookie";


const API_BASE_URL = "http://localhost:5110/api";

const axiosInstance = axios.create({
  baseURL: API_BASE_URL,
  headers: { 
    "Content-Type": "application/json",
    "Access-Control-Allow-Origin" : "http://localhost:5173",
    "Access-Control-Allow-Methods" : "*",
    "Access-Control-Allow-Credentials" : true
  }
  });

axiosInstance.interceptors.request.use(
  (config) => {
    const token = tokenService.getToken();
    console.log(token);
    if (token !== undefined) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    config.withCredentials = true
    return config;
  },
  (error) => Promise.reject(error)
);

axiosInstance.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      try {
        const refreshToken = Cookies.get("refreshToken")
        if (!refreshToken) throw new Error("No refresh token available");

        const { data } = await axios.post(`${API_BASE_URL}/RefreshToken/refresh`);
        

        originalRequest.headers.Authorization = `Bearer ${data.token}`;
        return axiosInstance(originalRequest);
      } catch {
        console.error("Token refresh failed, logging out.");
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
        window.location.href = "/login";
      }
    }

    return Promise.reject(error);
  }
);

export default axiosInstance;
