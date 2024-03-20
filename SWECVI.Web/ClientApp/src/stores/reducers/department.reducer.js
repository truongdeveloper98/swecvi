import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  departments: {
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

const departmentsSlice = createSlice({
  name: "department",
  initialState,
  reducers: {
    // request
    requested: (state) => {
      state.isLoading = true;
      state.error = undefined;
    },
    departmentsSuccess: (state, action) => {
      state.isLoading = false;
      state.departments = action.payload;
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

export const { requested, failed, succeed, departmentsSuccess, reinitialize } =
  departmentsSlice.actions;
export default departmentsSlice.reducer;
