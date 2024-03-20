import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  pythons: [],
  isLoading: false,
  success: undefined,
  error: undefined,
};

const pythonSlice = createSlice({
  name: "python",
  initialState,
  reducers: {
    requested: (state) => {
      state.isLoading = true;
      state.error = undefined;
    },
    pythonsSuccess: (state, action) => {
      state.isLoading = false;
      state.pythons = action.payload;
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

export const { requested, pythonsSuccess, failed, succeed, reinitialize } = pythonSlice.actions;

export default pythonSlice.reducer;
