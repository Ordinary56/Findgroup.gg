import { Route, BrowserRouter as Router, Routes, useLocation, Navigate, Outlet } from "react-router-dom";
import { useAuth, AuthProvider } from "./component/Auth_Context/AuthContext"; // Authentication logic
import Navbar from "./component/Navbar/Navbar";
import Footer from "./component/Footer/Footer";
import LandingPage from "./component/pages/LandingPage";
import LoginPage from "./component/pages/Login";
import Home from "./component/pages/Home";
import RegisterPage from "./component/pages/Register";
import CreateButton from "./component/Create_group_button/Create_group_button";
import CreateGroup from "./component/pages/CreateGroup";
import TopicDetails from "./component/pages/InspectListing";
import CreatorScreenAfterListing from "./component/pages/CreatorScreenAfterListnig";

// Routes configuration
export const ROUTES = {
  homepage: { path: "/", title: "Home" },
  register: { path: "/register", title: "Register" },
  login: { path: "/login", title: "Login" },
  create: { path: "/create", title: "Create" },
  landingpage:{ path: "/landingpage", title: "LandingPage" },
  aftercreate:{path:"/aftercreate", title:"CreatorScreenAfterListing"}
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
  const location = useLocation();
  return (
    <>
      <Navbar />
      <Routes>
        {/* Public routes */}
        <Route element={<PublicRoute />}>
          <Route path={ROUTES.login.path} element={<LoginPage />} />
          <Route path={ROUTES.register.path} element={<RegisterPage />} />
          <Route path="/landingpage" element={<LandingPage />} /> 
        </Route>

        {/* Private routes */}
        <Route element={<PrivateRoute/>}>
          <Route index element={<Home />} />
          <Route path={ROUTES.homepage.path} element={<Home />} />
          <Route path="/topics/:topicId" element={<TopicDetails />} />
          <Route path={ROUTES.create.path} element={<CreateGroup />} />
          <Route path={ROUTES.aftercreate.path} element={< CreatorScreenAfterListing/>} />
        </Route>

       
        
        
      </Routes>
      {/* Display CreateButton only on homepage */}
      {location.pathname === ROUTES.homepage.path && <CreateButton />}
      <Footer />
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