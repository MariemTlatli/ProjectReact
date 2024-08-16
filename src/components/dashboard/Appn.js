import React from 'react';
import Header from './Header';
import Sidebar from './sidebar';
import Dashboard from './dashboard';
import ServiceOrder from './ServiceOrder';
import TopCreators from './TopCreators';
import './Appn.css';

function Appn() {
    return (
        <div className="container">
            <Sidebar />
            <main className="main-content">
                <Header />
                <Dashboard />
                <ServiceOrder />
                <TopCreators />
            </main>
        </div>
    );
}

export default Appn;
