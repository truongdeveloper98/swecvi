import api from "services/api";

const apis = {
  hospitals: "SuperAdminHospital/hospitals",
  login: "auth/login",
};

const API = {
  hospitals: () => api.get(apis.hospitals),
  login: (email, password, hospitalId) => {
    const customHeader = {
      hospitalId,
    };
    return api.post(
      apis.login,
      { email, password },
      { headers: { ...api.defaults.headers, ...customHeader } }
    );
  },
};

export default API;
