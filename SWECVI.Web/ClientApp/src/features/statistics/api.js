import api from "services/api";

const apis = {
  parameterNames: "parameters-management/parameter-names",

  parameterValues: "parameters-management/parameter-values",

  findingNames: "hl7-management/code-meaning",

  findingValues: "hl7-management/code-meaning-chart",

  xSelectors: "parameters-management/x-selector",
};

const API = {
  parameterNames: () => api.get(apis.parameterNames),

  parameterValues: (params) => api.get(apis.parameterValues, { params }),

  findingNames: () => api.get(apis.findingNames),

  findingValues: (params) => api.get(apis.findingValues, { params }),

  xSelectors: () => api.get(apis.xSelectors),
};

export default API;
