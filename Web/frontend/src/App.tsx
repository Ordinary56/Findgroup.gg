import { Route, BrowserRouter as Router, Routes, useLocation } from "react-router-dom";
import Home from "./component/pages/Home";
import Crew from "./component/pages/Crew";
import RegisterPage from "./component/pages/Register";
import LoginPage from "./component/pages/Login";
import Navbar from "./component/Navbar/Navbar";
import Footer from "./component/Footer/Footer";
import CreateButton from "./component/Create_group_button/Create_group_button";
import CreateGroup from "./component/pages/CreateGroup";
import TopicDetails from "./component/pages/InspectListing";

export const ROUTES = {
  homepage: { path: "/", title: "Home" },
  crew: { path: "/crew", title: "Crew" },
  register: { path: "/register", title: "Register" },
  login: { path: "/login", title: "Login" },
  create: { path: "/create", title: "Create" },
};

const AppContent = () => {
  const location = useLocation();

  return (
    <>
      <Navbar />
      <Routes>
        <Route index element={<Home />} /> {/* Alapértelmezett útvonal */}
        <Route path="/" element={<Home />} />
        <Route path="/topics/:topicId" element={<TopicDetails />} />
        <Route path={ROUTES.crew.path} element={<Crew />} />
        <Route path={ROUTES.login.path} element={<LoginPage />} />
        <Route path={ROUTES.register.path} element={<RegisterPage />} />
        <Route path={ROUTES.create.path} element={<CreateGroup />} />
      </Routes>
      {/* Csak a főoldalon jelenjen meg a gomb */}
      {location.pathname === ROUTES.homepage.path && <CreateButton />}
      <Footer />
    </>
  );
};

const App = () => {
  return (
    <div className="background">
      <Router>
        <AppContent />
      </Router>
    </div>
  );
};

export default App;
