import api from "services/api";

const apis = {
  parameters: "parameters-management/parameters",

  parameter: (parameterId) => `parameters-management/parameters/${parameterId}`,

  functionSelectors: "parameters-management/function-selectors",
};

const API = {
  parameters: (params) => api.get(apis.parameters, { params }),

  parameter: (id, data) => api.put(apis.parameter(id), data),

  functionSelectors: () => api.get(apis.functionSelectors),
};

export default API;
