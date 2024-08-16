import React from 'react';
import Sidebar from './sidebar';
import Table from './table';
import './style.css';

const Layout = () => {
  return (
    <div className="container">
      <Sidebar />
      <main className="main-content">
        <div className="user--info">
          <input type="text" placeholder="Search" />
        </div>
        <section className="content">
          <h1>Articles</h1>
          <Table />
        </section>
      </main>
    </div>
  );
};

export default Layout;
