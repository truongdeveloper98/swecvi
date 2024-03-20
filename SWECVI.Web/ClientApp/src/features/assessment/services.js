import { store } from "stores";
import { assessmentsSuccess, failed, requested } from "stores/reducers/assessment.reducer";
import API from "./api";

export const getAssessmentRequest = async (params) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.assessments(params);
    if (response.data) {
      dispatch(assessmentsSuccess(response.data));
    }
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};
