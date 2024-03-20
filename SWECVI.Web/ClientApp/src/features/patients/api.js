import api from "services/api";

const apis = {
  patients: "PatientMirthConnects/patients",

  deletePatient: (patientId) => `patient-management/patients/${patientId}`,

  studies: (hospitalId, patientId) =>
    `PatientMirthConnects/patients/${hospitalId}/${patientId}/exams`,

  forceUpdate: "dicom-management/force-update?today=false",

  examPatients: "exampatient-management/exampatients",
  exportPatient: "patient-management/patients",
};

const API = {
  patients: (params) => api.get(apis.patients, { params }),

  studies: (hospitalId, id, params) => api.get(apis.studies(hospitalId, id), { params }),

  deletePatient: (id) => api.delete(apis.deletePatient(id)),

  forceUpdate: () => api.get(apis.forceUpdate),

  examPatients: () => api.put(apis.examPatients),

  exportPatient: (data) =>
    api.post(apis.exportPatient, data, {
      responseType: "arraybuffer",
    }),
};

export default API;
