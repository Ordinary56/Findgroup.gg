import React, { createContext, useContext, useState, useEffect, ReactNode, useRef } from "react";
import { apiService } from "../../api/apiService";
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
  const ranOnce = useRef(false);
  useEffect(() => {
    if(ranOnce.current) return;
    const checkValidation = async() => {
      const {data, status} : AxiosResponse<{valid : boolean}> = await axiosInstance.get("/Auth/validate-token");
      if(status !== 200) {
        setIsAuthenticated(false);
        await apiService.logout();
        console.log(data);
        return;
      }
      console.log(data)
      setIsAuthenticated(data.valid);
    }
    checkValidation();
    console.log(isAuthenticated);
    ranOnce.current = true;
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
