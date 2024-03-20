import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  isLoading: false,
  patients: {
    items: [],
    limit: undefined,
    page: undefined,
    totalItems: undefined,
    totalPages: undefined,
  },
  patientFindingBox: [],
  patientFindingValueInput: {
    valueOnlyCheckBox: {},
    valueOnlyRadioButton: {},
    valueMultiSelect: {},
    valueOneSelect: {},
    valueNumricInput: {},
    valueTextInput: {},
    valueDateInput: {},
  },
  patientCreateOrUpdate: [],
  diagram: [],
  error: undefined,
  success: undefined,
};

const slice = createSlice({
  name: "patient",
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

    patientsRequest: (state) => {
      state.isLoading = true;
      state.error = undefined;
      state.patients = {
        items: [],
        limit: undefined,
        page: undefined,
        totalItems: undefined,
        totalPages: undefined,
      };
    },

    patientsSuccess: (state, action) => {
      state.isLoading = false;
      state.patients = action.payload;
    },

    patientFindingBoxRequest: (state) => {
      state.isLoading = true;
      state.error = undefined;
      state.patientFindingBox = [];
    },

    setPatientFindingValueInput: (state, action) => {
      state.patientFindingValueInput = action.payload;
    },

    setPatientCreateOrUpdate: (state, action) => {
      state.patientCreateOrUpdate = action.payload;
    },

    patientFindingBoxSuccess: (state, action) => {
      state.isLoading = false;
      state.patientFindingBox = action.payload;
    },

    diagramRequest: (state) => {
      state.isLoading = false;
      state.diagram = [];
    },
    diagramSuccess: (state, action) => {
      state.isLoading = false;
      state.diagram = action.payload;
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

  patientsRequest,
  patientsSuccess,

  setPatientFindingValueInput,
  setPatientCreateOrUpdate,

  patientFindingBoxRequest,
  patientFindingBoxSuccess,

  diagramRequest,
  diagramSuccess,

  reinitialize,
} = slice.actions;

export default slice.reducer;
