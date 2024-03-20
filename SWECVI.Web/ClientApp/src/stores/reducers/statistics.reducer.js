import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  isLoading: false,
  parameterNames: [],
  parameterValues: [],
  findingNames: [],
  findingValues: [],
  xSelectors: [],
  error: undefined,
};

const statisticsSlice = createSlice({
  name: "statistic",
  initialState,
  reducers: {
    requested: (state) => {
      state.isLoading = true;
      state.error = undefined;
    },
    parameterNamesSuccess: (state, action) => {
      state.isLoading = false;
      state.parameterNames = action.payload;
    },
    parameterValuesSuccess: (state, action) => {
      state.isLoading = false;
      state.parameterValues = action.payload;
    },
    findingNamesSuccess: (state, action) => {
      state.isLoading = false;
      state.findingNames = action.payload;
    },
    findingValuesSuccess: (state, action) => {
      state.isLoading = false;
      state.findingValues = action.payload;
    },
    xSelectorsSuccess: (state, action) => {
      state.isLoading = false;
      state.xSelectors = action.payload;
    },
    failed: (state, action) => {
      state.isLoading = false;
      state.error = action.payload;
    },
  },
});

export const {
  requested,
  parameterNamesSuccess,
  parameterValuesSuccess,
  findingNamesSuccess,
  findingValuesSuccess,
  xSelectorsSuccess,
  failed,
} = statisticsSlice.actions;

export default statisticsSlice.reducer;
