import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  isLoading: false,
  pageSize: 10,
  period: undefined,
  patientFinding: "allmänt",
};

const slice = createSlice({
  name: "common",
  initialState,
  reducers: {
    setLoading: (state, action) => {
      state.isLoading = action.payload;
    },
    setPageSize: (state, action) => {
      state.pageSize = action.payload;
    },
    setPeriod: (state, action) => {
      state.period = action.payload;
    },
    setPatientFinding: (state, action) => {
      state.patientFinding = action.payload;
    },
  },
});

export const { setLoading, setPageSize, setPeriod, setPatientFinding } = slice.actions;

export default slice.reducer;
