import { store } from "stores";
import { failed, requested, findingStructureSuccess } from "stores/reducers/finding.reducer";
import API from "./api";

export const getFindingStructuresRequest = async (params) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.finding_structures(params);
    if (response.data) {
      dispatch(findingStructureSuccess(response.data));
    }
  } catch (error) {
    dispatch(failed(error));
  }
};
