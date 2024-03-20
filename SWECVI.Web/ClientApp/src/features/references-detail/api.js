import api from "services/api";

const apis = {
  references: (id) => `reference-management/references/${id}`,
  referencess: "reference-management/references",
};

const API = {
  updateReferences: (id, data) => api.put(apis.references(id), data),
  createReferences: (data) => api.post(apis.referencess, data),
  references: (id) => api.get(apis.references(id)),
};

export default API;
