import { Link } from "react-router-dom";
import { useState } from "react";
import { ROUTES } from "../../App";
import styles from "./navbar.module.css";
import Logo from "../../assets/Logo.png";
import { apiService } from "../../api/apiService";


const handleLogout = () => {
  apiService.logout();
};
const Navbar = () => {
  const [isMobile, setIsMobile] = useState<boolean>(window.innerWidth < 800);
  window.addEventListener("resize", () => {
    setIsMobile(window.innerWidth < 800);
  });

  return <>{isMobile ? <MobileNavbar /> : <DesktopNavbar />}</>;
};

const DesktopNavbar = () => {
  return (<>
    <nav className={styles.desktop}>
    <Link to={ROUTES.homepage.path} className={styles.logo}> 
        <img src={Logo} alt="Logo" />
      </Link>
      <div className={styles.links}>
        <Link to={ROUTES.homepage.path}>{ROUTES.homepage.title}</Link>
        <Link to={ROUTES.crew.path}>{ROUTES.crew.title}</Link>
        <Link to={ROUTES.login.path}>{ROUTES.login.title}</Link>
        <Link to={ROUTES.register.path}>{ROUTES.register.title}</Link>
        <button onClick={handleLogout}>Logout</button>
      </div>
    </nav></>
  );
};

const MobileNavbar = () => {
  // 🔹 Itt adunk meg egyértelmű típust!
  const [menuOpen, setMenuOpen] = useState<boolean>(false);

  return (
    <nav className={styles.mobile}>
      <div className={`${styles.hambi} ${menuOpen ? styles.open : ""}`} onClick={() => setMenuOpen(!menuOpen)}></div>
      <div className={`${styles.links} ${menuOpen ? styles.open : ""}`}>
        <Link to={ROUTES.homepage.path} onClick={() => setMenuOpen(false)}>
          {ROUTES.homepage.title}
        </Link>
        <Link to={ROUTES.crew.path} onClick={() => setMenuOpen(false)}>
          {ROUTES.crew.title}
        </Link>
        <Link to={ROUTES.login.path} onClick={() => setMenuOpen(false)}>
          {ROUTES.login.title}
        </Link>
        <Link to={ROUTES.register.path} onClick={() => setMenuOpen(false)}>
          {ROUTES.register.title}
        </Link>
        <button onClick={handleLogout}>Logout</button>
      </div>
    </nav>
  );
};

export default Navbar;
