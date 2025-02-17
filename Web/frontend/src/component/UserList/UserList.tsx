import { use } from "react";
import { User } from "../../api/Models/User";
// TODO: Work on this component
const UserList = ({ userPromise }) => {
  const users: User[] = use<User[]>(userPromise);
  return  (
    <ul>
        {users.map(user => ( 
            <li key={user.id}></li>
        ))}
    </ul>
  )
};

export default UserList;
