import api from "services/api";

const apis = {
  examReport: (hospitalId, examId) => `study-management/studies/${hospitalId}/${examId}/report`,

  examHL7Measurements: (examId) => `exam-management/exams/${examId}/hl7-measurements`,

  diagram: "parameters-management/parameter-values-exam",

  exam: (id) => `study-management/studies/${id}`,

  updateParameters: (id) => `parameters-management/parameters-function/${id}`,
};

const API = {
  examReport: (hospitalId, id) => api.get(apis.examReport(hospitalId, id)),
  examHL7Measurements: (id) => api.get(apis.examHL7Measurements(id)),
  diagram: (params) => api.get(apis.diagram, { params }),
  exam: (id) => api.get(apis.exam(id)),
  updateParameters: (id, params) => api.put(apis.updateParameters(id), null, { params }),
};

export default API;
