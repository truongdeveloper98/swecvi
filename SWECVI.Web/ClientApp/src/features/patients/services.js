/* eslint-disable no-bitwise */
import { store } from "stores";
import { setLoading } from "stores/reducers/common.reducer";
import {
  requested,
  failed,
  patientsRequest,
  patientsSuccess,
  succeed,
} from "stores/reducers/patient.reducer";
import API from "./api";

export const getPatientsRequest = async (params) => {
  const { dispatch } = store;
  try {
    dispatch(patientsRequest());
    const qr = { ...params, dicomPatientId: params.textSearch };
    const request = await API.patients(qr);
    if (request.data) {
      request.data?.items?.forEach((element) => {
        element.name = element.name?.split("^")?.reverse()?.join(" ") ?? "-";
      });
      dispatch(patientsSuccess(request.data));
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const getStudiesByPatientIdRequest = async (hospitalId, patientId, params, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const request = await API.studies(hospitalId, patientId, params);
    if (request.data) {
      if (callback) callback(request.data);
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const getStudiesByPatientId = async (hospitalId, patientId) => {
  const patient = await API.studies(hospitalId, patientId);
  return patient;
};

export const deletePatientRequest = async (patientId, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.deletePatient(patientId);
    dispatch(succeed("Patient deleted successfully"));
    if (callback) {
      callback();
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const forceSyncPatientsRequest = async (callback) => {
  const { dispatch } = store;
  try {
    dispatch(setLoading(true));
    await API.forceUpdate();
    dispatch(setLoading(false));
    dispatch(succeed("Sync patients data successfully"));
    if (callback) {
      callback();
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  } finally {
    dispatch(setLoading(false));
  }
};

export const examPatientsRequest = async () => {
  const { dispatch } = store;
  try {
    dispatch(setLoading(true));
    await API.examPatients();
    dispatch(setLoading(false));
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const exportPatientRequest = async (data) => {
  const { dispatch } = store;
  try {
    await API.exportPatient(data)
      .then((res) => {
        const url = window.URL.createObjectURL(
          new Blob([res.data], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;",
          })
        );
        const link = document.createElement("a");
        link.href = url;
        link.setAttribute("download", "ParameterResult.xlsx");
        document.body.appendChild(link);
        link.click();
      })
      .catch((err) => dispatch(failed(err?.response?.data)));
    dispatch(succeed("Export patient data successfully"));
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};
