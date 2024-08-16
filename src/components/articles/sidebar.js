import React from 'react';
import './Sidebar.css';
import { Link } from 'react-router-dom';

function Sidebar() {
  return (
    <aside className="sidebar">
      <div className="logo">
        <hr />
      </div>
      <ul className="menu">
      <li>
        <Link to="/">
          <i className="fas fa-home"></i> Dashboard
        </Link>
      </li>
      <li>
        <Link to="/service-order">
          <i className="fas fa-tasks"></i> Service Order
        </Link>
      </li>
      <li>
        <Link to="/users">
          <i className="fas fa-users"></i> Users
        </Link>
      </li>
      <li>
        <Link to="/companies">
          <i className="fas fa-building"></i> Companies
        </Link>
      </li>
      <li>
        <Link to="/article">
          <i className="fas fa-newspaper"></i> Articles
        </Link>
      </li>
    </ul>
    </aside>
  );
}

export default Sidebar;
