import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  assessments: {
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

const assessmentsSlice = createSlice({
  name: "assessments",
  initialState,
  reducers: {
    // request
    requested: (state) => {
      state.isLoading = true;
      state.error = undefined;
    },
    assessmentsSuccess: (state, action) => {
      state.isLoading = false;
      state.assessments = action.payload;
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

export const { requested, failed, succeed, assessmentsSuccess, reinitialize } =
  assessmentsSlice.actions;
export default assessmentsSlice.reducer;
