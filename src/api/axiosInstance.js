// src/api/axiosInstance.js
import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "http://localhost:5224/api", // Replace with your API URL
  headers: {
    "Content-Type": "application/json",
  },
});

export default axiosInstance;
