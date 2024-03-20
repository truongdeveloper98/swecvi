import { store } from "stores";
import { failed, requested, usersSuccess } from "stores/reducers/user.reducer";
import API from "./api";

export const activeUserRequest = async (id, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.activeUser(id);
    if (callback) callback(response.data);
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};

export const inactiveUserRequest = async (id, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.inactiveUser(id);
    if (callback) callback(response.data);
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};

export const getUsersRequest = async (params) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.users(params);
    if (response.data) {
      dispatch(usersSuccess(response.data));
    }
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};
