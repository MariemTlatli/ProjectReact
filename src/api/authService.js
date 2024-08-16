// src/api/authService.js
import axiosInstance from "./axiosInstance";

export const registerAdmin = async (registerData) => {
  try {
    const response = await axiosInstance.post(
      "/account/registerAdmin",
      registerData
    );
    return response.data;
  } catch (error) {
    throw error.response.data || "Registration failed";
  }
};

export const registerUser = async (registerData) => {
  try {
    const response = await axiosInstance.post(
      "/account/registerUser",
      registerData
    );
    return response.data;
  } catch (error) {
    throw error.response.data || "Registration failed";
  }
};

export const loginUser = async (loginData) => {
  try {
    const response = await axiosInstance.post("/account/login", loginData);
    return response.data;
  } catch (error) {
    throw error.response.data || "Login failed";
  }
};
