import { Link } from "react-router-dom"
import { ROUTES } from "../../App"

const CreateButton = () => {
  return (
      <button>    
          
      <Link to={ROUTES.register.path}> Create a FindGroup</Link>
      </button>     
  )
}

export default CreateButton