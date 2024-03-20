import { store } from "stores";
import { departmentsSuccess, failed, requested } from "stores/reducers/department.reducer";
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
