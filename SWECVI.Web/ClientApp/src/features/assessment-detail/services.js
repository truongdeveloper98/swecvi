import { store } from "stores";
import { failed, requested, succeed } from "stores/reducers/assessment.reducer";
import API from "./api";

export const updateAssessmentRequest = async (id, data, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.updateAssessment(id, data);
    dispatch(succeed("Assessment updated successfully"));
    if (callback) callback();
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};

export const createAssessmentRequest = async (data, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.createAssessment(data);
    dispatch(succeed("Assessment created successfully"));
    if (callback) callback();
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};

export const getAssessmentRequest = async (id, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.assessment(id);
    if (response.data) {
      if (callback) callback(response.data);
    }
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};
