import api from "services/api";

const apis = {
  assessments: "assessment-management/assessments",
};

const API = {
  assessments: (params) => api.get(apis.assessments, { params }),
};

export default API;
