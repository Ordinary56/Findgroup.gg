import axios, { AxiosResponse } from "axios";
const API_BASE_URL = "http://localhost:5110/api";
const axiosInstance = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
    "Access-Control-Allow-Origin": "http://localhost:5173",
    "Access-Control-Allow-Methods": "*",
    "Access-Control-Allow-Credentials": true
  }
});

axiosInstance.interceptors.request.use(
  (config) => {
    config.withCredentials = true
    return config;
  },
  (error) => Promise.reject(error)
);

axiosInstance.interceptors.response.use(
  (response) => response,
  async (error) => {
    console.log("Interceptor has been hit");
    const originalRequest = error.config;
    if (error.response?.status === 401 && !originalRequest._retry) {
      try {
        const { data }: AxiosResponse = await axios.post(`${API_BASE_URL}/RefreshToken/refresh`);
        console.log(data);
        return axiosInstance(originalRequest);
      } catch {
        console.error("Token refresh failed, logging out.");
        await axios.post(`${API_BASE_URL}/Auth/logout`);
        window.location.href = "/login";
      }
      originalRequest._retry = true;
    }

    return Promise.reject(error);
  }
);

export default axiosInstance;
