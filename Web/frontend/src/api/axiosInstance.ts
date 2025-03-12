import axios, { AxiosResponse } from "axios";
const API_BASE_URL = "http://localhost:5110/api";
const axiosInstance = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
    "Access-Control-Allow-Credentials": true,
  },
  withCredentials : true
  
});

axiosInstance.interceptors.request.use(
  (config) => {
    config.withCredentials = true
    // config.headers.Accept = "application/json"

    return config;
  },
  (error) => Promise.reject(error)
);

axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => {
    console.log("Interceptor has been hit");
    console.log(error);
    console.log(axios.isAxiosError(error));

    const originalRequest = error.config;

    // Ha nincs válasz, vagy egyéb hiba történt
    if (!error.response) {
      console.error("Network error or no response received.");
      return Promise.reject(error);
    }

    // Ha 401-es státuszkódú válasz érkezik, próbálkozunk a token frissítéssel
    if (error.response?.status === 401 && !originalRequest._retry) {
      console.error("Try refresh");

      return axios
        .post(`${API_BASE_URL}/RefreshToken/refresh`)
        .then((response) => {
          console.log(response.data);
          // Ha sikerült a frissítés, újraküldjük az eredeti kérést
          originalRequest._retry = true;
          return axiosInstance(originalRequest);
        })
        .catch((refreshError) => {
          console.error("Token refresh failed, logging out.", refreshError);
          return axios
            .post(`${API_BASE_URL}/Auth/logout`)
            .then(() => {
              window.location.href = "/login";
            })
            .catch((logoutError) => {
              console.error("Logout failed.", logoutError);
            });
        });
    }

    return Promise.reject(error);
  }
);


export default axiosInstance;
