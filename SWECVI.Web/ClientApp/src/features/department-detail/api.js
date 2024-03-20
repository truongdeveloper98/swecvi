import api from "services/api";

const apis = {
  department: (id) => `department-management/departments/${id}`,
  departments: "department-management/departments",
  hospitals: "SuperAdminHospital/hospitals",
};

const API = {
  updateDepartment: (id, data) => api.put(apis.department(id), data),
  createDepartment: (data) => api.post(apis.departments, data),
  department: (id) => api.get(apis.department(id)),
  getHospitals: () => api.get(apis.hospitals),
};

export default API;
