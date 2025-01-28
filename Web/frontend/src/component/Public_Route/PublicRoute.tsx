import React from "react";
import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../../component/Guest_page/AuthContext";

const PublicRoute: React.FC = () => {
  const { isAuthenticated } = useAuth();

  return isAuthenticated ? <Navigate to="/" /> : <Outlet />;
};

export default PublicRoute;
