import React from "react";
import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../Auth_Context/AuthContext";

const PublicRoute: React.FC = () => {
  const { isAuthenticated } = useAuth();

  return isAuthenticated ? <Navigate to="/" /> : <Outlet />;
};

export default PublicRoute;
