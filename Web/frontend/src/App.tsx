import { Route, BrowserRouter as Router, Routes } from "react-router-dom"
import Home from "./component/pages/Home"
import Crew from "./component/pages/Crew"
import RegisterPage from "./component/pages/Register"
import LoginPage from "./component/pages/Login"
import Navbar from "./component/Navbar/Navbar"
import Footer from "./component/Footer/Footer"
import CreateButton from "./component/Create_group_button/Create_group_button"


export const ROUTES ={
  "homepage": {path:"/" ,title:"Home"},
  "crew": {path:"/crew" ,title:"Crew"},
  "register": {path:"/register" ,title:"Register"},
  "login": {path:"/login" ,title:"Login"},
}


const App = () => {
  return ( 
    <div className="background">
      <Router>
        <Navbar/>
        <Routes>
          <Route path={ROUTES.homepage.path} element={<Home/>}/>
          <Route path={ROUTES.crew.path} element={<Crew/>}/>
          <Route path={ROUTES.login.path} element={<LoginPage/>}/>
          <Route path={ROUTES.register.path} element={<RegisterPage/>}/>
        </Routes>  
      </Router>
      
      <Router>
      <CreateButton/>
      <Routes>
      <Route path={ROUTES.register.path} element={<RegisterPage/>}/>
      </Routes>
      </Router>

      <Footer/>
    </div>
    
  )
}

export default App