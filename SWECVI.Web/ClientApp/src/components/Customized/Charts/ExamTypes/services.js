import { store } from "stores";
import { requested, failed, examTypesSuccess } from "stores/reducers/analytic.reducer";
import API from "./api";

export const getExamTypesRequest = async (params) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const request = await API.examTypes(params);
    if (request.data) {
      dispatch(examTypesSuccess(request.data));
    }
  } catch (error) {
    dispatch(failed(error?.response?.data?.title));
  }
};
