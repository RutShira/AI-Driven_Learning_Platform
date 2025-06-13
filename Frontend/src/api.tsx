// api.ts
import axios from 'axios';

const BASE_URL = 'http://localhost:5292/api';

export const getRequest = async (url: string, config = {}) => {
  const res = await axios.get(`http://localhost:5292/api${url}`, config);
  return res.data;
};

export const postRequest = async (url: string, data: any, config = {}) => {
  const res = await axios.post(`${BASE_URL}${url}`, data, config);
  return res.data;
};

// login APIs (without token)
export const loginUser = async (username: string, phone: string) => {
  const response = await axios.post(`${BASE_URL}/Auth/login`, { username, phone });
  return response.data;
};

export const loginAdmin = async (username: string, password: string) => {
  const response = await axios.post(`${BASE_URL}/Auth/login`, { username, password });
  return response.data;
};
