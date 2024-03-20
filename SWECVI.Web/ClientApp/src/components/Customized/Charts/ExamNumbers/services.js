import { store } from "stores";
import { requested, failed, examNumbersSuccess } from "stores/reducers/analytic.reducer";
import API from "./api";

export const getExamNumbersRequest = async (params) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const request = await API.examNumbers(params);
    if (request.data) {
      dispatch(examNumbersSuccess(request.data));
    }
  } catch (error) {
    dispatch(failed(error?.response?.data?.title));
  }
};
