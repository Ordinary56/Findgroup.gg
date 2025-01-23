const API_BASE_URL = "http://localhost:5000/api";

export const apiService = {
  // Token kezelés
  setToken: (token: string) => {
    localStorage.setItem("accessToken", token);
  },
  getToken: (): string | null => {
    return localStorage.getItem("accessToken");
  },
  setRefreshToken: (token: string) => {
    localStorage.setItem("refreshToken", token);
  },
  getRefreshToken: (): string | null => {
    return localStorage.getItem("refreshToken");
  },
  clearTokens: () => {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
  },

  // Bejelentkezés
  login: async (username: string, password: string): Promise<void> => {
    const response = await fetch(`${API_BASE_URL}/Auth/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ username, password }),
    });

    if (!response.ok) {
      throw new Error("Bejelentkezés sikertelen");
    }

    const data = await response.json();
    apiService.setToken(data.token);
    apiService.setRefreshToken(data.refreshToken);
  },

  // Regisztráció
  register: async (username: string, password: string, email?: string): Promise<void> => {
    const response = await fetch(`${API_BASE_URL}/Auth/register`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ username, password, email }),
    });

    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(errorData.message || "Regisztráció sikertelen");
    }
  },

  // Token frissítés
  refreshToken: async (): Promise<void> => {
    const refreshToken = apiService.getRefreshToken();
    if (!refreshToken) {
      throw new Error("Nincs elérhető frissítő token");
    }

    const response = await fetch(`${API_BASE_URL}/RefreshToken/refresh`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ token: refreshToken }),
    });

    if (!response.ok) {
      throw new Error("Token frissítési hiba");
    }

    const data = await response.json();
    apiService.setToken(data.token);
    apiService.setRefreshToken(data.refreshToken);
  },

  // API hívások
  fetchWithAuth: async (endpoint: string, options: RequestInit = {}): Promise<Response> => {
    const token = apiService.getToken();

    const headers = new Headers(options.headers);
    if (token) {
      headers.append("Authorization", `Bearer ${token}`);
    }

    let response = await fetch(`${API_BASE_URL}${endpoint}`, {
      ...options,
      headers,
    });

    // Ha a token lejárt, próbáljuk frissíteni
    if (response.status === 401) {
      try {
        await apiService.refreshToken();
        const newToken = apiService.getToken();

        // Ismételt kérés az új tokennel
        headers.set("Authorization", `Bearer ${newToken}`);
        response = await fetch(`${API_BASE_URL}${endpoint}`, {
          ...options,
          headers,
        });
      } catch (error) {
        apiService.clearTokens();
        console.error(error);
        throw new Error("Token frissítés sikertelen. Jelentkezz be újra.");
      }
    }

    return response;
  },
};
