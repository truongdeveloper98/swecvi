import { store } from "stores";
import {
  failed,
  findingNamesSuccess,
  findingValuesSuccess,
  parameterNamesSuccess,
  parameterValuesSuccess,
  requested,
  xSelectorsSuccess,
} from "stores/reducers/statistics.reducer";
import API from "./api";

export const parameterNamesRequest = async () => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.parameterNames();
    if (response?.data) {
      dispatch(parameterNamesSuccess(response?.data));
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const parameterValuesRequest = async (params) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.parameterValues(params);
    if (response?.data) {
      dispatch(parameterValuesSuccess(response?.data));
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const findingNamesRequest = async () => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.findingNames();
    if (response?.data) {
      dispatch(findingNamesSuccess(response?.data));
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const findingValuesRequest = async (params, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.findingValues(params);
    if (response?.data) {
      dispatch(findingValuesSuccess(response?.data));
      if (callback) {
        callback(response?.data);
      }
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const xSelectorsRequest = async () => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.xSelectors();
    if (response?.data) {
      dispatch(xSelectorsSuccess(response?.data));
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};
