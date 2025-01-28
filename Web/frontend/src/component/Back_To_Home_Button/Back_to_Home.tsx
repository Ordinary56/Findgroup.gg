import { Link } from "react-router-dom"
import { ROUTES } from "../../App"

const BackToHomeButton = () => {
  return (
      <div>        
      <Link to={ROUTES.homepage.path}>Back to Home</Link>
      </div>     
  )
}

export default BackToHomeButton