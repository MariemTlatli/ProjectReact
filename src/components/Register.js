import React, { useState } from "react";
import { registerUser } from "../api/authService"; // Import the service function
import { useNavigate, Link } from "react-router-dom"; // Import Link and useNavigate
import "./Login.css"; // Import your custom CSS file

const Register = () => {
  const [formData, setFormData] = useState({
    username: "",
    email: "",
    phoneNumber: "",
    password: "",
  });
  const [error, setError] = useState(null);
  const navigate = useNavigate(); // Initialize useNavigate

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const data = await registerUser(formData);
      console.log("User registered:", data);
      navigate("/login"); // Navigate to login page after successful registration
    } catch (err) {
      setError(err.message);
    }
  };

  return (
    <section className="vh-100">
      <div className="container-fluid h-custom">
        <div className="row d-flex justify-content-center align-items-center h-100">
          <div className="col-md-9 col-lg-6 col-xl-5">
            <h1>Register Page</h1>
            <img
              src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-login-form/draw2.webp"
              className="img-fluid"
              alt="Sample"
            />
          </div>
          <div className="col-md-8 col-lg-6 col-xl-4 offset-xl-1">
            <br></br>
            <br></br>
            <form onSubmit={handleSubmit}>
            
              {/* Username input */}
              <div className="form-outline mb-4">
                <input
                  type="text"
                  id="form3Example1"
                  name="username"
                  className="form-control form-control-lg"
                  placeholder="Enter your username"
                  onChange={handleChange}
                  required
                />
                <label className="form-label" htmlFor="form3Example1">
                  Username
                </label>
              </div>

              {/* Email input */}
              <div className="form-outline mb-4">
                <input
                  type="email"
                  id="form3Example2"
                  name="email"
                  className="form-control form-control-lg"
                  placeholder="Enter a valid email address"
                  onChange={handleChange}
                  required
                />
                <label className="form-label" htmlFor="form3Example2">
                  Email address
                </label>
              </div>

              {/* Phone Number input */}
              <div className="form-outline mb-4">
                <input
                  type="text"
                  id="form3Example3"
                  name="phoneNumber"
                  className="form-control form-control-lg"
                  placeholder="Enter your phone number"
                  onChange={handleChange}
                  required
                />
                <label className="form-label" htmlFor="form3Example3">
                  Phone Number
                </label>
              </div>

              {/* Password input */}
              <div className="form-outline mb-3">
                <input
                  type="password"
                  id="form3Example4"
                  name="password"
                  className="form-control form-control-lg"
                  placeholder="Enter password"
                  onChange={handleChange}
                  required
                />
                <label className="form-label" htmlFor="form3Example4">
                  Password
                </label>
              </div>

              {error && <p className="text-danger">{error}</p>}

              <div className="text-center text-lg-start mt-4 pt-2">
                <button
                  type="submit"
                  className="btn btn-primary btn-lg"
                  style={{ paddingLeft: "2.5rem", paddingRight: "2.5rem" }}
                >
                  Register
                </button>
                <p className="small fw-bold mt-2 pt-1 mb-0">
                  Already have an account?{" "}
                  <Link to="/login" className="link-danger">
                    Login
                  </Link>
                </p>
              </div>
            </form>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Register;
