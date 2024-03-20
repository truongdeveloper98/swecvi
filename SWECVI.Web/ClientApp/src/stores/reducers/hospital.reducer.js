import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  hospitals: {
    items: [],
    limit: undefined,
    page: undefined,
    totalItems: undefined,
    totalPages: undefined,
  },
  region: "",
  isLoading: false,
  error: undefined,
  success: undefined,
};

const hospitalsSlice = createSlice({
  name: "hospital",
  initialState,
  reducers: {
    // request
    requested: (state) => {
      state.isLoading = true;
      state.error = undefined;
    },
    setRegion: (state, action) => {
      state.region = action.payload;
    },
    hospitalsSuccess: (state, action) => {
      state.isLoading = false;
      state.hospitals = action.payload;
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

export const { requested, failed, succeed, hospitalsSuccess, reinitialize, setRegion } =
  hospitalsSlice.actions;
export default hospitalsSlice.reducer;
