/* eslint-disable no-console */
import { store } from "stores";
import {
  examDetailRequest,
  examDetailSuccess,
  examHL7MeasurementsRequest,
  examHL7MeasurementsSuccess,
  examReportRequest,
  examReportSuccess,
  requested,
  succeed,
} from "stores/reducers/exam.reducer";
import { failed, diagramRequest, diagramSuccess } from "stores/reducers/patient.reducer";
import API from "./api";

export const getHL7MeasurementByExamIdRequest = async (examId) => {
  const { dispatch } = store;
  try {
    dispatch(examHL7MeasurementsRequest());
    const hl7MeasurementsRequest = await API.examHL7Measurements(examId);
    if (hl7MeasurementsRequest?.data) {
      dispatch(examHL7MeasurementsSuccess(hl7MeasurementsRequest.data));
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const getExamReportByExamIdRequest = async (hospitalId, examId) => {
  const { dispatch } = store;
  try {
    dispatch(examReportRequest());
    const reportRequest = await API.examReport(hospitalId, examId);
    if (reportRequest?.data) {
      dispatch(examReportSuccess(reportRequest.data));
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const getDiagramRequest = async (params) => {
  const { dispatch } = store;
  try {
    dispatch(diagramRequest());
    const response = await API.diagram(params);
    dispatch(diagramSuccess(response.data));
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const getExamRequest = async (id) => {
  const { dispatch } = store;
  try {
    dispatch(examDetailRequest());
    const response = await API.exam(id);
    console.log(response);
    dispatch(examDetailSuccess(response.data));
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const updateParametersRequest = async (id, params, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.updateParameters(id, params);
    if (response?.status === 200) {
      dispatch(succeed("update function success!"));
      if (callback) {
        callback();
      }
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};
