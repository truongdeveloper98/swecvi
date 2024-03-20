import api from "services/api";

const apis = {
  hospital: (id) => `hospital-management/hospitals/${id}`,
  hospitals: "hospital-management/hospitals",
};

const API = {
  updateHospital: (id, data) => api.put(apis.hospital(id), data),
  createHospital: (data) => api.post(apis.hospitals, data),
  hospital: (id) => api.get(apis.hospital(id)),
};

export default API;
