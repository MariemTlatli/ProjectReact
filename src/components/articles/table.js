import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import '@fortawesome/fontawesome-free/css/all.min.css';
import './table.css';

const Table = () => {
  const itemsPerPage = 6;
  const [items, setItems] = useState([
    { id: 1, name: 'Item 1', price: 45, stock: 'available' },
    { id: 2, name: 'Item 2', price: 45, stock: 'available' },
    { id: 3, name: 'Item 3', price: 45, stock: 'available' },
    { id: 4, name: 'Item 4', price: 25, stock: 'available' },
    { id: 5, name: 'Item 5', price: 15, stock: 'out of stock' },
    { id: 6, name: 'Item 6', price: 35, stock: 'available' },
    { id: 7, name: 'Item 7', price: 50, stock: 'available' },
    { id: 8, name: 'Item 8', price: 30, stock: 'out of stock' },
    { id: 9, name: 'Item 9', price: 60, stock: 'available' },
    { id: 10, name: 'Item 10', price: 20, stock: 'available' },
  ]);

  const [currentPage, setCurrentPage] = useState(1);
  const [editingItem, setEditingItem] = useState(null);
  const [newName, setNewName] = useState('');
  const [newPrice, setNewPrice] = useState('');
  const [newId, setNewId] = useState('');

  const navigate = useNavigate();

  const handleAddItem = () => {
    navigate('/submit');
  };

  const handleDelete = (id) => {
    const filteredItems = items.filter(item => item.id !== id);
    setItems(filteredItems);
  };

  const handleEdit = (item) => {
    setEditingItem(item);
    setNewName(item.name);
    setNewPrice(item.price);
    setNewId(item.id);
  };

  const handleSave = () => {
    const updatedItems = items.map(item =>
      item.id === editingItem.id
        ? { ...item, id: newId, name: newName, price: newPrice }
        : item
    );
    setItems(updatedItems);
    setEditingItem(null);
  };

  const handleCancel = () => {
    setEditingItem(null);
  };

  const toggleStock = (id) => {
    const updatedItems = items.map(item =>
      item.id === id ? { ...item, stock: item.stock === 'available' ? 'out of stock' : 'available' } : item
    );
    setItems(updatedItems);
  };

  // Pagination logic
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentItems = items.slice(indexOfFirstItem, indexOfLastItem);

  const totalPages = Math.ceil(items.length / itemsPerPage);

  const handleNextPage = () => {
    if (currentPage < totalPages) {
      setCurrentPage(currentPage + 1);
    }
  };

  const handlePreviousPage = () => {
    if (currentPage > 1) {
      setCurrentPage(currentPage - 1);
    }
  };

  return (
    <div>
      <table className="table">
        <thead>
          <tr>
            <th style={{ textAlign: 'center' }}>ID</th>
            <th style={{ textAlign: 'center' }}>Name</th>
            <th style={{ textAlign: 'center' }}>Price</th>
            <th style={{ textAlign: 'center' }}>Operate</th>
            <th style={{ textAlign: 'center' }}>State</th>
          </tr>
        </thead>
        <tbody>
          {currentItems.map(item => (
            <tr key={item.id}>
              <td style={{ textAlign: 'center' }}>{item.id}</td>
              <td style={{ textAlign: 'center' }}>
                {editingItem && editingItem.id === item.id ? (
                  <input
                    type="text"
                    value={newName}
                    onChange={(e) => setNewName(e.target.value)}
                  />
                ) : (
                  item.name
                )}
              </td>
              <td style={{ textAlign: 'center' }}>
                {editingItem && editingItem.id === item.id ? (
                  <input
                    type="number"
                    value={newPrice}
                    onChange={(e) => setNewPrice(e.target.value)}
                  />
                ) : (
                  item.price
                )}
              </td>
              <td style={{ textAlign: 'center' }}>
                {editingItem && editingItem.id === item.id ? (
                  <>
                    <button className='button-save' onClick={handleSave}>
                      <i className="fas fa-save"></i>
                    </button>
                    <button className='button-cancel' onClick={handleCancel}>
                      <i className="fas fa-times"></i>
                    </button>
                  </>
                ) : (
                  <>
                    <button className='button-delete' onClick={() => handleDelete(item.id)}>
                      <i className="fas fa-trash-alt"></i>
                    </button>
                    <button className='button-edit' onClick={() => handleEdit(item)}>
                      <i className="fas fa-edit"></i>
                    </button>
                  </>
                )}
              </td>
              <td style={{ textAlign: 'center' }}>
                <button
                  className={item.stock === 'available' ? 'button-stock-available' : 'button-stock-out'}
                  onClick={() => toggleStock(item.id)}
                >
                  {item.stock}
                </button>
              </td>
            </tr>
          ))}
        </tbody>
        <tfoot>
          <tr>
            <td colSpan="5" className="pagination-footer">
              <div className="pagination-content">
                <button
                  className="pagination-button"
                  onClick={handlePreviousPage}
                  disabled={currentPage === 1}
                >
                  <i className="fas fa-chevron-left"></i> Previous
                </button>
                <span>Page {currentPage} of {totalPages}</span>
                <button
                  className="pagination-button"
                  onClick={handleNextPage}
                  disabled={currentPage === totalPages}
                >
                  Next <i className="fas fa-chevron-right"></i>
                </button>
              </div>
            </td>
          </tr>
        </tfoot>
      </table>

      <div className="add-item-form">
        <button className='button-add' onClick={handleAddItem}>
          <i className="fas fa-plus"></i> Add Item
        </button>
      </div>
    </div>
  );
};

export default Table;
