import React from 'react';
import './Sidebar.css';



function Sidebar() {
  return (
    <aside className="sidebar">
      <div className="logo">

        
        <hr />
      </div>
      <ul className="menu">
        <li><a href="/"><i className="fas fa-home"></i> Dashboard</a></li>
        <li><a href="#"><i className="fas fa-tasks"></i> Service Order</a></li>
        <li><a href="#"><i className="fas fa-users"></i> Users</a></li>
        <li><a href="#"><i className="fas fa-building"></i> Companies</a></li>
        <li><a href="article"><i className="fas fa-newspaper"></i> Articles</a></li>
        <li><a href="/people"><i className="fas fa-home"></i> People</a></li>

      </ul>
    </aside>
  );
}

export default Sidebar;
