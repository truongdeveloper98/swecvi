import api from "services/api";

const apis = {
  examNumbers: "exam-management/exam-numbers",
};

const API = {
  examNumbers: (params) => api.get(apis.examNumbers, { params }),
};

export default API;
