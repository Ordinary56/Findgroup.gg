import axios from "axios";

// Backend API URL
const API_BASE_URL = "http://localhost:5000/api";

// Axios példány létrehozása
const axiosInstance = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

// ✅ REQUEST INTERCEPTOR (Automatikusan hozzáadja a JWT-t minden kéréshez)
axiosInstance.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("accessToken");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// ✅ RESPONSE INTERCEPTOR (401-es hibánál megpróbálja frissíteni a tokent)
axiosInstance.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.response?.status === 401) {
      try {
        const refreshToken = localStorage.getItem("refreshToken");
        if (!refreshToken) throw new Error("No refresh token available");

        const refreshResponse = await axios.post(`${API_BASE_URL}/RefreshToken/refresh`, {
          token: refreshToken,
        });

        const newToken = refreshResponse.data.token;
        const newRefreshToken = refreshResponse.data.refreshToken;

        localStorage.setItem("accessToken", newToken);
        localStorage.setItem("refreshToken", newRefreshToken);

        // Újraküldjük az eredeti kérést az új tokennel
        error.config.headers.Authorization = `Bearer ${newToken}`;
        return axiosInstance(error.config);
      } catch (refreshError) {
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
        window.location.href = "/login"; // Kijelentkeztetés
        return Promise.reject(refreshError);
      }
    }
    return Promise.reject(error);
  }
);

export default axiosInstance;
