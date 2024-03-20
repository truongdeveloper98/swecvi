import api from "services/api";

const apis = {
  departments: "department-management/departments",
};

const API = {
  departments: (params) => api.get(apis.departments, { params }),
};

export default API;
