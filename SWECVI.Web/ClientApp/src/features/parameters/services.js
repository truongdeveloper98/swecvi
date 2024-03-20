import { store } from "stores";
import {
  failed,
  requested,
  succeed,
  parametersSuccess,
  functionSelectorsSuccess,
} from "stores/reducers/parameter.reducer";
import API from "./api";

export const getParametersRequest = async (params) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.parameters(params);
    if (response.data) {
      dispatch(parametersSuccess(response.data));
    }
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};

export const updateParameterRequest = async (id, data, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.parameter(id, data);
    dispatch(succeed("Parameter updated successfully"));
    if (callback) callback();
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};

export const functionSelectorsRequest = async () => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.functionSelectors();
    if (response?.data) {
      dispatch(functionSelectorsSuccess(response?.data));
    }
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};
