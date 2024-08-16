
import React, { useState } from 'react';
import '@fortawesome/fontawesome-free/css/all.min.css'; 
import './dashboard.css';

function Dashboard()  {
    return (
        <section className="dashboard">
            <h1>Dashboard</h1>
            <h2>Welcome to your dashboard</h2>

            <div className="stats">
                <div className="stat-box">
                    <p><b>12,361</b></p>
                    <span>Service <br /> accomplished</span>
                    <span className="stat-change">+14%</span>
                </div>
                <div className="stat-box">
                    <p><b>431,225</b></p>
                    <span>Service in progress</span>
                    <span className="stat-change">+27%</span>
                </div>
                <div className="stat-box">
                    <p><b>32,441</b></p>
                    <span>Number of companies</span>
                    <span className="stat-change">+5%</span>
                </div>
                <div className="stat-box">
                    <p><b>1,325,134</b></p>
                    <span>Users number</span>
                    <span className="stat-change">+43%</span>
                </div>
            </div>
        </section>
    );
}

export default Dashboard;
