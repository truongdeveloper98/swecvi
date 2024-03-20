import api from "services/api";

const apis = {
  findingStructure: "finding-structure",
  studyFinding: "study-finding",
  updateStudyFinding: (hospitalId) => `study-finding/${hospitalId}`,
  getStudyFinding: (studyId) => `study-finding/${studyId}`,
};

const API = {
  findingStructure: () => api.get(apis.findingStructure),
  studyFinding: (model) => api.post(apis.studyFinding, model),
  updateStudyFinding: (hospitalId, model) => api.put(apis.updateStudyFinding(hospitalId), model),
  getStudyFinding: (studyId) => api.get(apis.getStudyFinding(studyId)),
};

export default API;
