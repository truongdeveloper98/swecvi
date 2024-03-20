import { store } from "stores";
import { failed, referencesSuccess, requested } from "stores/reducers/references.reducer";
import API from "./api";

export const getReferencesRequest = async (params) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.referencess(params);
    if (response.data) {
      dispatch(referencesSuccess(response.data));
    }
  } catch (error) {
    dispatch(failed(error));
  }
};
