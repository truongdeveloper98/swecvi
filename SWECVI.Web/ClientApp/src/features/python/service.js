import { store } from "stores";
import { failed, pythonsSuccess, requested, succeed } from "stores/reducers/python.reducer";
import API from "./api";

export const getPythonsRequest = async () => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.pythons();
    if (response?.data) {
      dispatch(pythonsSuccess(response?.data));
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const deletePythonRequest = async (id, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.deletePython(id);
    dispatch(succeed("Python code deleted successfully!"));
    if (callback) {
      callback();
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const resetDefaultPythonCodeRequest = async (id, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.resetDefault(id);
    dispatch(succeed("Reset python code to default version successfully"));
    if (callback) {
      callback();
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const setAsDefaultPythonCodeRequest = async (id, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.pythonCurrent(id);
    dispatch(succeed("Set this python code to default version successfully"));
    if (callback) {
      callback();
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const createPythonRequest = async (id, params, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.createPython(id, params);
    dispatch(succeed("Python code created successfully!"));
    if (callback) {
      callback();
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};
