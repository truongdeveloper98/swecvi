import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  isLoading: false,
  examNumbers: [],
  examTypes: [],
  error: undefined,
};

const slice = createSlice({
  name: "analytic",
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
    examNumbersSuccess: (state, action) => {
      state.isLoading = false;
      state.examNumbers = action.payload;
    },
    examTypesSuccess: (state, action) => {
      state.isLoading = false;
      state.examTypes = action.payload;
    },
  },
});

export const { requested, failed, examNumbersSuccess, examTypesSuccess } = slice.actions;

export default slice.reducer;
