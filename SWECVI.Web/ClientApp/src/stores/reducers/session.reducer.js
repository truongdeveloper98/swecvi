import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  isLoading: false,
  sessions: {
    items: [],
    limit: undefined,
    page: undefined,
    totalItems: undefined,
    totalPages: undefined,
  },
  sessionsFields: [],

  error: undefined,
  success: undefined,
};

const slice = createSlice({
  name: "session",
  initialState,
  reducers: {
    requested: (state) => {
      state.isLoading = true;
      state.error = undefined;
    },
    failed: (state, action) => {
      state.isLoading = false;
      state.error = action.payload;
    },
    succeed: (state, action) => {
      state.isLoading = false;
      state.success = action.payload;
    },
    sessionsSuccess: (state, action) => {
      state.isLoading = false;
      state.sessions = action.payload;
    },
    sessionsFieldSuccess: (state, action) => {
      state.isLoading = false;
      state.sessionsFields = action.payload;
    },

    reinitialize: (state) => {
      state.error = undefined;
      state.success = undefined;
    },
  },
});

export const { requested, failed, succeed, sessionsSuccess, sessionsFieldSuccess, reinitialize } =
  slice.actions;

export default slice.reducer;
