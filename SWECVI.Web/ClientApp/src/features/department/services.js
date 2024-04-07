/* eslint-disable no-console */
import { store } from "stores";
import { departmentsSuccess, failed, requested, succeed } from "stores/reducers/department.reducer";
import API from "./api";

export const getDepartmentRequest = async (params) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.departments(params);
    if (response.data) {
      dispatch(departmentsSuccess(response.data));
    }
  } catch (error) {
    dispatch(failed(error));
  }
};

export const deleteDepartmentRequest = async (id, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.deleteDepartment(id);
    dispatch(succeed("Department deleted successfully"));
    if (callback) {
      callback();
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};
