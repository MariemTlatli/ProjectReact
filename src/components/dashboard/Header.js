import React, { useState } from 'react';
import '@fortawesome/fontawesome-free/css/all.min.css'; 
import './Header.css';

function Header() {
    return (
        <main className="main-content">
            <header>
                <div className="search-box">
                    <input type="text" placeholder="Search" />
                    <i className="fas fa-search search-icon"></i>
                </div>
                <div className="admin">
                    <i className="fas fa-user-shield"></i> 
                    <span>Admin</span>
                </div>
            </header>
        </main>
    );
}

export default Header;
