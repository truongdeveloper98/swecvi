import api from "services/api";

const apis = {
  examTypes: "exam-management/exam-types",
};

const API = {
  examTypes: (params) => api.get(apis.examTypes, { params }),
};

export default API;
