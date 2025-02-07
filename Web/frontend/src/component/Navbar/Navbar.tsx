import { Link } from "react-router-dom";
import { useState } from "react";
import { useAuth } from "../Auth_Context/AuthContext"; // Importáljuk az auth hookot
import { ROUTES } from "../../App";
import styles from "./navbar.module.css";
import Logo from "../../assets/Logo.png";

const Navbar = () => {
  const { isAuthenticated, logout } = useAuth();
  const [isMobile, setIsMobile] = useState<boolean>(window.innerWidth < 768);
  
  window.addEventListener("resize", () => {
    setIsMobile(window.innerWidth < 768);
  });

  return <>{isMobile ? <MobileNavbar isAuthenticated={isAuthenticated} logout={logout} /> : <DesktopNavbar isAuthenticated={isAuthenticated} logout={logout} />}</>;
};

const DesktopNavbar: React.FC<{ isAuthenticated: boolean; logout: () => void }> = ({ isAuthenticated, logout }) => {
  return (
    <nav className={styles.desktop}>
      <Link to={ROUTES.homepage.path} className={styles.logo}> 
        <img src={Logo} alt="Logo" />
      </Link>
      <div className={styles.links}>
        <Link to={ROUTES.homepage.path}>{ROUTES.homepage.title}</Link>
       
        {!isAuthenticated && (
          <>
            <Link to={ROUTES.login.path}>{ROUTES.login.title}</Link>
            <Link to={ROUTES.register.path}>{ROUTES.register.title}</Link>
          </>
        )}
        {isAuthenticated && <button className={styles.buttonstyle} onClick={logout}>Logout</button>}
      </div>
    </nav>
  );
};

const MobileNavbar: React.FC<{ isAuthenticated: boolean; logout: () => void }> = ({ isAuthenticated, logout }) => {
  const [menuOpen, setMenuOpen] = useState<boolean>(false);

  return (
    <nav className={styles.mobile}>
      <div className={`${styles.hambi} ${menuOpen ? styles.open : ""}`} onClick={() => setMenuOpen(!menuOpen)}></div>
      <div className={`${styles.links} ${menuOpen ? styles.open : ""}`}>
        <Link to={ROUTES.homepage.path} onClick={() => setMenuOpen(false)}>{ROUTES.homepage.title}</Link>
       
        {!isAuthenticated && (
          <>
            <Link to={ROUTES.login.path} onClick={() => setMenuOpen(false)}>{ROUTES.login.title}</Link>
            <Link to={ROUTES.register.path} onClick={() => setMenuOpen(false)}>{ROUTES.register.title}</Link>
          </>
        )}
        {isAuthenticated && <button className={styles.mobilebuttonstyle} onClick={logout}>Logout</button>}
      </div>
    </nav>
  );
};

export default Navbar;
