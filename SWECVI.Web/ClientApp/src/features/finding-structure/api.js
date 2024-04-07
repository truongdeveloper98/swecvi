import api from "services/api";

const apis = {
  finding_structures: "findingstructure-management/findingstructures",
};

const API = {
  finding_structures: (params) => api.get(apis.finding_structures, { params }),
};

export default API;
