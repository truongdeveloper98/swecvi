import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  settings: {
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

const settingsSlice = createSlice({
  name: "settings",
  initialState,
  reducers: {
    // request
    requested: (state) => {
      state.isLoading = true;
      state.error = undefined;
    },
    settingsSuccess: (state, action) => {
      state.isLoading = false;
      state.settings = action.payload;
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

export const { requested, failed, succeed, settingsSuccess, reinitialize } = settingsSlice.actions;
export default settingsSlice.reducer;
