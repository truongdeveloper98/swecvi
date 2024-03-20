/* eslint-disable new-cap */
import { store } from "stores";
import { setAuth, failed, setDataHospitals } from "stores/reducers/auth.reducer";
import API from "./api";

export const loginRequest = async ({ email, password, hospitalId }) => {
  const { dispatch } = store;
  try {
    const request = await API.login(email, password, hospitalId);
    dispatch(setAuth(request.data));
  } catch (error) {
    dispatch(failed(error?.response?.data?.title));
  }
};

export const hospitalData = async () => {
  const { dispatch } = store;
  try {
    const request = await API.hospitals();
    // request.data.push({ id: 0, name: "Super Admin Hospital", connectionString: null });
    dispatch(setDataHospitals(request.data));
  } catch (error) {
    dispatch(failed(error?.response?.data?.title));
  }
};
