import api from "services/api";

const apis = {
  departments: "department-management/departments",
  deleteDepartment: (id) => `department-management/departments/${id}`,
};

const API = {
  departments: (params) => api.get(apis.departments, { params }),
  deleteDepartment: (id) => api.delete(apis.deleteDepartment(id)),
};

export default API;
