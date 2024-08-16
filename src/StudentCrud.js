import axios from "axios";
import { useEffect, useState } from "react";

function UserList() {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    async function Load() {
      try {
        const result = await axios.get("http://localhost:5224/api/users/test");
        setUsers(result.data);
        console.log(result.data);
      } catch (err) {
        console.error("Error fetching users", err);
      }
    }
    Load();
  }, []);

  return (
    <div>
      <h1>User Details</h1>
      <table className="table table-dark" align="center">
        <thead>
          <tr>
            <th scope="col">User Id</th>
            <th scope="col">Name</th>
            <th scope="col">Email</th>
            <th scope="col">Phone Number</th> {/* Added Phone Number column */}
            <th scope="col">Role</th>
          </tr>
        </thead>
        {users.map((user) => (
          <tbody key={user.id}>
            <tr>
              <th scope="row">{user.id}</th>
              <td>{user.userName}</td>{" "}
              {/* Corrected from user.name to user.userName */}
              <td>{user.email}</td>
              <td>{user.phoneNumber}</td> {/* Added Phone Number data */}
              <td>{user.role}</td>
            </tr>
          </tbody>
        ))}
      </table>
    </div>
  );
}

export default UserList;
