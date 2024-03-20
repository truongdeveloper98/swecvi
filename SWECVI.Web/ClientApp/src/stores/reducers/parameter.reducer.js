import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  parameters: {
    items: [],
    limit: undefined,
    page: undefined,
    totalItems: undefined,
    totalPages: undefined,
  },
  functionSelectors: [],
  isLoading: false,
  error: undefined,
  success: undefined,
};

const parametersSlice = createSlice({
  name: "parameter",
  initialState,
  reducers: {
    // request
    requested: (state) => {
      state.isLoading = true;
      state.error = undefined;
    },
    parametersSuccess: (state, action) => {
      state.isLoading = false;
      state.parameters = action.payload;
    },
    failed: (state, action) => {
      state.isLoading = false;
      state.error = action.payload;
    },
    succeed: (state, action) => {
      state.isLoading = false;
      state.success = action.payload;
    },
    reinitialize: (state) => {
      state.error = undefined;
      state.success = undefined;
    },
    functionSelectorsSuccess: (state, action) => {
      state.functionSelectors = action.payload;
    },
  },
});

export const {
  requested,
  failed,
  succeed,
  parametersSuccess,
  reinitialize,
  functionSelectorsSuccess,
} = parametersSlice.actions;

export default parametersSlice.reducer;
