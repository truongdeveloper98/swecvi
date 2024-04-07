import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  findings: {
    items: [],
    limit: undefined,
    page: undefined,
    totalItems: undefined,
    totalPages: undefined,
  },
  isLoading: false,
  error: undefined,
  success: undefined,
};

const findingsSlice = createSlice({
  name: "findings",
  initialState,
  reducers: {
    // request
    requested: (state) => {
      state.isLoading = true;
      state.error = undefined;
    },
    findingStructureSuccess: (state, action) => {
      state.isLoading = false;
      state.findings = action.payload;
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
  },
});

export const { requested, failed, succeed, findingStructureSuccess, reinitialize } =
  findingsSlice.actions;
export default findingsSlice.reducer;
