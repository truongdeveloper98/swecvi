import api from "services/api";

const apis = {
  Findings: (id) => `Ffindingstructure-management/findingstructures/${id}`,
  Findingss: "findingstructure-management/findingstructures",
};

const API = {
  updateFindings: (id, data) => api.put(apis.Findings(id), data),
  createFindings: (data) => api.post(apis.Findingss, data),
  Findings: (id) => api.get(apis.Findings(id)),
};

export default API;
