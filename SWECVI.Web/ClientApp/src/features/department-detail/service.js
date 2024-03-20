import { store } from "stores";
import { failed, requested, succeed } from "stores/reducers/department.reducer";
import API from "./api";

export const updateDepartmentRequest = async (id, data, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.updateDepartment(id, data);
    dispatch(succeed("Department updated successfully"));
    if (callback) callback();
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};

export const createDepartmentRequest = async (data, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.createDepartment(data);
    dispatch(succeed("Department created successfully"));
    if (callback) callback();
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};

export const getDepartmentRequest = async (id, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.department(id);
    if (response.data) {
      if (callback) callback(response.data);
    }
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};

export const getHospitalsRequest = async () => {
  const hospitals = await API.getHospitals();
  return hospitals.data;
};
