import { Route, BrowserRouter as Router, Routes } from "react-router-dom"
import Home from "./compontents/pages/Home"
import Crew from "./compontents/pages/Crew"
import Destination from "./compontents/pages/Destination"
import Technology from "./compontents/pages/Technology"
import Navbar from "./compontents/Navbar/Navbar"


export const ROUTES ={
  "homepage": {path:"/" ,title:"Home"},
  "crew": {path:"/crew" ,title:"Crew"},
  "destination": {path:"/destination" ,title:"Destination"},
  "technology": {path:"/technology" ,title:"Technology"},
}

const App = () => {
  return (
    <div className="background">
    <Router>
      <Navbar/>
      <Routes>
        <Route path={ROUTES.homepage.path} element={<Home/>}/>
        <Route path={ROUTES.crew.path} element={<Crew/>}/>
        <Route path={ROUTES.destination.path} element={<Destination/>}/>
        <Route path={ROUTES.technology.path} element={<Technology/>}/>
      </Routes>  
    </Router>
    </div>
  )
}

export default App