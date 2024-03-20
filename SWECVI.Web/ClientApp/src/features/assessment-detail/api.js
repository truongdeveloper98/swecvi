import api from "services/api";

const apis = {
  assessment: (id) => `assessment-management/assessments/${id}`,

  assessments: "assessment-management/assessments",
};

const API = {
  updateAssessment: (id, data) => api.put(apis.assessment(id), data),

  createAssessment: (data) => api.post(apis.assessments, data),

  assessment: (id) => api.get(apis.assessment(id)),
};

export default API;
