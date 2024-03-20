import { store } from "stores";
import { failed, hospitalsSuccess, requested, setRegion } from "stores/reducers/hospital.reducer";
import API from "./api";

const regions = [
  { id: 0, value: "Sweden" },
  { id: 1, value: "VietNam" },
];

export const getHospitalRequest = async (params) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.hospitals(params);
    if (response.data) {
      dispatch(hospitalsSuccess(response.data));
    }
  } catch (error) {
    dispatch(failed(error));
  }
};

export const getHospitalRequestById = async (id) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.hospital(id);
    if (response.data) {
      dispatch(setRegion(regions.find((item) => item.id === response.data.indexRegion).value));
    }
  } catch (error) {
    dispatch(failed(error.response?.data));
  }
};
