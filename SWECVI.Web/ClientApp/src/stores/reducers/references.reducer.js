import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  references: {
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

const referencesSlice = createSlice({
  name: "references",
  initialState,
  reducers: {
    // request
    requested: (state) => {
      state.isLoading = true;
      state.error = undefined;
    },
    referencesSuccess: (state, action) => {
      state.isLoading = false;
      state.references = action.payload;
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

export const { requested, failed, succeed, referencesSuccess, reinitialize } =
  referencesSlice.actions;
export default referencesSlice.reducer;
