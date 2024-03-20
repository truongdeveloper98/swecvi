import axios from "axios";
import qs from "qs";
import PAGES from "navigation/pages";
import { store } from "stores";
import { LOGOUT } from "constants/actionTypes";

const api = axios.create({
  baseURL: process.env?.REACT_APP_API_URL,
  timeout: 0,
  headers: {
    "Content-Type": "application/json",
  },
  paramsSerializer: (params) =>
    qs.stringify(params, {
      arrayFormat: "repeat",
    }),
});

api.interceptors.request.use(
  async (config) => {
    const token = store.getState().auth?.token;
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

api.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.response && error.response.status === 401) {
      const { dispatch } = store;
      dispatch({ type: LOGOUT });
      window.location.href = PAGES.signIn;
    }
    return Promise.reject(error);
  }
);
export default api;
