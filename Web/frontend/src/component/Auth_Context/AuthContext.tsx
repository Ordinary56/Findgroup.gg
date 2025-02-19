import React, { createContext, useContext, useState, useEffect, ReactNode } from "react";
import { apiService } from "../../api/apiService";
import { tokenService } from "../../api/tokenService";
import axiosInstance from "../../api/axiosInstance";
import { AxiosResponse } from "axios";

type AuthContextType = {
  isAuthenticated: boolean;
  login: () => void;
  logout: () => void;
};

const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);

  useEffect(() => {
    // TODO: rework authentication check
    const checkValidation = async() => {
      const {data} : AxiosResponse = await axiosInstance.get("/Auth/validate-token");
      setIsAuthenticated(data.Valid);
    }
    checkValidation();
  }, []);

  const login = () => {
    setIsAuthenticated(true);
  };

  const logout = () => {
    apiService.logout();
    setIsAuthenticated(false);
  };

  return (
    <AuthContext.Provider value={{ isAuthenticated, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
