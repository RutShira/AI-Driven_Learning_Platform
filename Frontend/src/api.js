import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5292/api',
  timeout: 10000,
});

export const getRequest = async (url, config = {}) => {
  const res = await axios.get(`http://localhost:5292/api${url}`, config);
  return res.data;
};

export const postRequest = async (url, data) => {
  const res = await axios.post(`http://localhost:5292/api${url}`, data);
  return res.data;
};