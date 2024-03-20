import api from "services/api";

const apis = {
  referencess: "reference-management/references",
};

const API = {
  referencess: (params) => api.get(apis.referencess, { params }),
};

export default API;
