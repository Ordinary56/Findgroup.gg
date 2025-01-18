import { Route, BrowserRouter as Router, Routes } from "react-router-dom"
import Home from "./component/pages/Home"
import Crew from "./component/pages/Crew"
import Destination from "./component/pages/Destination"
import Technology from "./component/pages/Technology"
import Navbar from "./component/Navbar/Navbar"
import Footer from "./component/Footer/Footer"
import LoginPage from "./component/Login/Login"


export const ROUTES ={
  "homepage": {path:"/" ,title:"Home"},
  "crew": {path:"/crew" ,title:"Crew"},
  "destination": {path:"/destination" ,title:"Destination"},
  "technology": {path:"/technology" ,title:"Technology"},
}


const App = () => {
  return ( 
    <div className="background">
      <LoginPage/>
      <Router>
        <Navbar/>
        <Routes>
          <Route path={ROUTES.homepage.path} element={<Home/>}/>
          <Route path={ROUTES.crew.path} element={<Crew/>}/>
          <Route path={ROUTES.destination.path} element={<Destination/>}/>
          <Route path={ROUTES.technology.path} element={<Technology/>}/>
        </Routes>  
      </Router>
      <Footer/>
    </div>
    
  )
}

export default App