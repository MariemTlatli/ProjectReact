import React, { useState } from "react";
import { loginUser } from "../api/authService"; // Import the login service function
import { useNavigate, Link } from "react-router-dom"; // Import Link and useNavigate
import "./Login.css"; // Import your custom CSS file


const Login = () => {
  const [formData, setFormData] = useState({
    username: "",
    email: "",
    password: "",
  });
  const [errors, setErrors] = useState({});
  const [showPopup, setShowPopup] = useState(false); // State for success popup
  const [showErrorPopup, setShowErrorPopup] = useState(false); // State for error popup

  const navigate = useNavigate(); // Initialize useNavigate

  const validateForm = () => {
    let formErrors = {};
    let isValid = true;

    // Username validation
    if (!formData.username.trim()) {
      formErrors.username = "Username is required";
      isValid = false;
    } else if (formData.username.length < 3) {
      formErrors.username = "Username must be at least 3 characters long";
      isValid = false;
    }

    // Email validation
    if (!formData.email.trim()) {
      formErrors.email = "Email is required";
      isValid = false;
    } else if (!/\S+@\S+\.\S+/.test(formData.email)) {
      formErrors.email = "Email is not valid";
      isValid = false;
    }

    // Password validation
    if (!formData.password.trim()) {
      formErrors.password = "Password is required";
      isValid = false;
    } else if (formData.password.length < 6) {
      formErrors.password = "Password must be at least 6 characters";
      isValid = false;
    } else if (!/[A-Z]/.test(formData.password)) {
      formErrors.password = "Password must contain at least one uppercase letter";
      isValid = false;
    } else if (!/[0-9]/.test(formData.password)) {
      formErrors.password = "Password must contain at least one number";
      isValid = false;
    } else if (!/[!@#$%^&*]/.test(formData.password)) {
      formErrors.password = "Password must contain at least one special character";
      isValid = false;
    }

    setErrors(formErrors);
    return isValid;
  };

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!validateForm()) {
      return; // Prevent form submission if validation fails
    }

    try {
      // Reset error state before login attempt
      setErrors({});
      setShowErrorPopup(false);

      const data = await loginUser(formData);
      console.log("User logged in:", data);
      setShowPopup(true); // Show success popup on successful login

      // Redirect to another page on successful login
      navigate("/"); // Replace '/dashboard' with your desired route
    } catch (err) {
      setErrors({ global: err.message });
      setShowErrorPopup(true); // Show error popup on failed login
    }
  };

  return (
    <section className="vh-100">
      <div className="container-fluid h-custom">
        <div className="row d-flex justify-content-center align-items-center h-100">
          <div className="col-md-9 col-lg-6 col-xl-5">
          <h1>Login Page</h1>

            <img
              src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-login-form/draw2.webp"
              className="img-fluid"
              alt="Sample"
            />
          </div>
          <div className="col-md-8 col-lg-6 col-xl-4 offset-xl-1">
            <br></br>
            <br></br>
            <br></br>
            <form onSubmit={handleSubmit}>

             

              {/* Username input */}
              <div className="form-outline mb-4">
                <input
                  type="text"
                  id="form3Example1"
                  name="username"
                  className={`form-control form-control-lg ${
                    errors.username ? "is-invalid" : ""
                  }`}
                  placeholder="Enter your username"
                  onChange={handleChange}
                  value={formData.username}
                />
                <label className="form-label" htmlFor="form3Example1">
                  Username
                </label>
                {errors.username && (
                  <div className="invalid-feedback">{errors.username}</div>
                )}
              </div>

              {/* Email input */}
              <div className="form-outline mb-4">
                <input
                  type="email"
                  id="form3Example2"
                  name="email"
                  className={`form-control form-control-lg ${
                    errors.email ? "is-invalid" : ""
                  }`}
                  placeholder="Enter a valid email address"
                  onChange={handleChange}
                  value={formData.email}
                />
                <label className="form-label" htmlFor="form3Example2">
                  Email address
                </label>
                {errors.email && (
                  <div className="invalid-feedback">{errors.email}</div>
                )}
              </div>

              {/* Password input */}
              <div className="form-outline mb-3">
                <input
                  type="password"
                  id="form3Example3"
                  name="password"
                  className={`form-control form-control-lg ${
                    errors.password ? "is-invalid" : ""
                  }`}
                  placeholder="Enter password"
                  onChange={handleChange}
                  value={formData.password}
                />
                <label className="form-label" htmlFor="form3Example3">
                  Password
                </label>
                {errors.password && (
                  <div className="invalid-feedback">{errors.password}</div>
                )}
              </div>

              {errors.global && <p className="text-danger">{errors.global}</p>}

              <div className="d-flex justify-content-between align-items-center">
                <div className="form-check mb-0">
                  <input
                    className="form-check-input me-2"
                    type="checkbox"
                    value=""
                    id="form2Example4"
                  />
                  <label className="form-check-label" htmlFor="form2Example4">
                    Remember me
                  </label>
                </div>
                <a href="#!" className="text-body">
                  Forgot password?
                </a>
              </div>

              <div className="text-center text-lg-start mt-4 pt-2">
                <button
                  type="submit"
                  className="btn btn-primary btn-lg"
                  style={{ paddingLeft: "2.5rem", paddingRight: "2.5rem" }}
                >
                  Login
                </button>
                <p className="small fw-bold mt-2 pt-1 mb-0">
                  Don't have an account?{" "}
                  <Link to="/Register" className="link-danger">
                    Register
                  </Link>
                </p>
              </div>
            </form>
          </div>
        </div>
      </div>

      {/* Success Popup Modal */}
      {showPopup && (
        <div
          className="modal fade show d-block"
          tabIndex="-1"
          style={{ display: "block", backgroundColor: "rgba(0, 0, 0, 0.5)" }}
        >
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">Success</h5>
                <button
                  type="button"
                  className="btn-close"
                  onClick={() => setShowPopup(false)}
                ></button>
              </div>
              <div className="modal-body">
                <p>You have successfully logged in!</p>
              </div>
              <div className="modal-footer">
                <button
                  type="button"
                  className="btn btn-secondary"
                  onClick={() => setShowPopup(false)}
                >
                  Close
                </button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Error Popup Modal */}
      {showErrorPopup && (
        <div
          className="modal fade show d-block"
          tabIndex="-1"
          style={{ display: "block", backgroundColor: "rgba(0, 0, 0, 0.5)" }}
        >
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">Error</h5>
                <button
                  type="button"
                  className="btn-close"
                  onClick={() => setShowErrorPopup(false)}
                ></button>
              </div>
              <div className="modal-body">
                <p>{errors.global}</p>
              </div>
              <div className="modal-footer">
                <button
                  type="button"
                  className="btn btn-secondary"
                  onClick={() => setShowErrorPopup(false)}
                >
                  Close
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </section>
  );
};

export default Login;
