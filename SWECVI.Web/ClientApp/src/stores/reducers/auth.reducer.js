import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  email: undefined,
  firstName: "",
  id: undefined,
  lastName: "",
  roles: [],
  token: undefined,
  error: undefined,
  hospitals: [],
  // authorization: [],
};

const slice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setAuth: (state, action) => {
      state.email = action.payload.email;
      state.firstName = action.payload.firstName;
      state.id = action.payload.id;
      state.lastName = action.payload.lastName;
      state.roles = action.payload.roles;
      state.token = action.payload.token;
    },

    // setAuthorization: (state, action) => {
    //   state.authorization = action.payload;
    // },

    setDataHospitals: (state, action) => {
      state.hospitals = action.payload;
    },

    failed: (state, action) => {
      state.error = action.payload;
    },

    reinitialize: (state) => {
      state.error = undefined;
    },
  },
});

export const { setAuth, failed, reinitialize, setDataHospitals } = slice.actions;

export default slice.reducer;
