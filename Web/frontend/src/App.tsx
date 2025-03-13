import { Route, BrowserRouter as Router, Routes, useLocation, Navigate, Outlet } from "react-router-dom";
import { useAuth, AuthProvider } from "./component/Auth_Context/AuthContext"; // Authentication logic
import Navbar from "./component/Navbar/Navbar";

import LandingPage from "./pages/Landing_Page/LandingPage";
import LoginPage from "./pages/Login/Login";
import Home from "./pages/Home/Home";
import RegisterPage from "./pages/Register/Register";
import CreateGroup from "./pages/Group_Creation/CreateGroup";
import Post from "./pages/Post/Post";
import Group from "./pages/Group/Group";
import Profile from "./pages/Profile/Profile";

// Routes configuration
export const ROUTES = {
  homepage: { path: "/", title: "Home" },
  register: { path: "/register", title: "Register" },
  login: { path: "/login", title: "Login" },
  create: { path: "/create", title: "Create" },
  landingpage: { path: "/landingpage", title: "LandingPage" },
  post: { path: "/post", title: "View Post" },
  group: { path: "/group/:groupId", title: "Group" },
  profile: { path: "/profile/:id", title: "User's info" }
};

const PublicRoute: React.FC = () => {
  const { isAuthenticated } = useAuth();
  return isAuthenticated ? <Navigate to={ROUTES.homepage.path} /> : <Outlet />;
};

const PrivateRoute: React.FC = () => {
  const { isAuthenticated } = useAuth();
  return isAuthenticated ? <Outlet /> : <Navigate to={ROUTES.landingpage.path} />;
};

const AppContent = () => {
  return (
    <>
      <Navbar/>
      <Routes>
        {/* Public routes */}
        <Route element={<PublicRoute />}>
          <Route path={ROUTES.login.path} element={<LoginPage />} />
          <Route path={ROUTES.register.path} element={<RegisterPage />} />
          <Route path="/landingpage" element={<LandingPage />} />
        </Route>

        {/* Private routes */}
        <Route element={<PrivateRoute />}>
          <Route index element={<Home />} />
          <Route path={ROUTES.homepage.path} element={<Home />} />
          <Route path={ROUTES.create.path} element={<CreateGroup />} />
          <Route path={ROUTES.post.path} element={<Post />} />
          <Route path={ROUTES.group.path} element={<Group />} />
          <Route path={ROUTES.profile.path} element={<Profile />} />
        </Route>
      </Routes>
    </>
  );
};

const App = () => {
  return (
    <div>
      <AuthProvider>
        <Router>
          <AppContent />
        </Router>
      </AuthProvider>
    </div>
  );
};

export default App;
