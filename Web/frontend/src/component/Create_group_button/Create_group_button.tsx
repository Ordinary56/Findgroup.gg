import { Link } from "react-router-dom"
import { ROUTES } from "../../App"

const CreateButton = () => {
  return (
      <div>        
      <Link to={ROUTES.create.path}> Create a FindGroup</Link>
      </div>     
  )
}

export default CreateButton