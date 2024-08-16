// App.js
import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./components/Login";
import Register from "./components/Register";
import Dashboard from "./components/dashboard/dashboard";
import SubmitForm from "./components/articles/submit";

import Appn from './components/dashboard/Appn';
import Articles from './components/articles/Article';
import People from './components/people/people';

import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import "@fortawesome/fontawesome-free/css/all.min.css";

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/dashboard" element={<Dashboard />} />
          <Route path="/" element={<Appn />} />
        <Route path="/submit" element={<SubmitForm />} />
        <Route path="/article" element={<Articles />} />
        <Route path="/people" element={<People />} />
          {/* Ajoutez d'autres routes ici */}
        </Routes>
      </div>
    </Router>
  );
}

export default App;
