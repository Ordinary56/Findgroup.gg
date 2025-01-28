import { Link } from "react-router-dom"
import { ROUTES } from "../../App"
import styles from "./navbar.module.css"
import { useState } from "react"
import Logo from "../../assets/Logo.png"

const Navbar = () => {
    const [ismobile,setmobile]= useState(window.innerWidth<800)
    
    window.addEventListener("resize",()=>{
        setmobile(window.innerWidth<800)
    })
    
    return (
        <>
    {ismobile? <MobileNavbar/>:<DesktopNavbar/>}
    </>
  )
}

const DesktopNavbar = () => {
    return (
      <nav className={styles.desktop}>
        <img src={Logo} alt=""  className={styles.logo}/>
          <div>
          <Link to={ROUTES.homepage.path}>{ROUTES.homepage.title}</Link>
          <Link to={ROUTES.crew.path}>{ROUTES.crew.title}</Link>

          <Link to={ROUTES.login.path}>{ROUTES.login.title}</Link>
          <Link to={ROUTES.register.path}>{ROUTES.register.title}</Link>

          </div>
      </nav>
    )
  }
  const MobileNavbar = () => {
    return (
        <nav className={styles.mobile}>
        <div className={styles.hambi}>
            <div className={styles.links}>
            <Link to={ROUTES.homepage.path}>{ROUTES.homepage.title}</Link>
            <Link to={ROUTES.crew.path}>{ROUTES.crew.title}</Link>

            <Link to={ROUTES.login.path}>{ROUTES.login.title}</Link>
            <Link to={ROUTES.register.path}>{ROUTES.register.title}</Link>

            </div>
        </div>
    </nav>
    )
  }


export default Navbar