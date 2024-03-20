import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  isLoading: false,

  examReport: undefined,
  examHL7Measurements: [],

  targetExam: undefined,

  error: undefined,
  success: undefined,
};

const slice = createSlice({
  name: "exam",
  initialState,
  reducers: {
    // request
    requested: (state) => {
      state.isLoading = true;
      state.error = undefined;
    },
    failed: (state, action) => {
      state.isLoading = false;
      state.error = action.payload;
    },
    succeed: (state, action) => {
      state.isLoading = false;
      state.success = action.payload;
    },

    examDetailRequest: (state) => {
      state.isLoading = false;
      state.error = undefined;
      state.targetExam = undefined;
    },
    examDetailSuccess: (state, action) => {
      state.isLoading = false;
      state.targetExam = action.payload;
    },

    examReportRequest: (state) => {
      state.isLoading = false;
      state.error = undefined;
      state.examReport = undefined;
    },
    examReportSuccess: (state, action) => {
      state.isLoading = false;
      state.examReport = action.payload;
    },

    examHL7MeasurementsRequest: (state) => {
      state.isLoading = false;
      state.error = undefined;
      state.examHL7Measurements = [];
    },
    examHL7MeasurementsSuccess: (state, action) => {
      state.isLoading = false;
      state.examHL7Measurements = action.payload;
    },

    // end request

    reinitialize: (state) => {
      state.error = undefined;
      state.success = undefined;
    },
  },
});

export const {
  requested,
  failed,
  succeed,

  examDetailRequest,
  examDetailSuccess,

  examReportRequest,
  examReportSuccess,

  examHL7MeasurementsRequest,
  examHL7MeasurementsSuccess,

  reinitialize,
} = slice.actions;

export default slice.reducer;
