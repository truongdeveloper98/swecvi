import api from "services/api";

const apis = {
  hospital: (id) => `hospital-management/hospitals/${id}`,
  hospitals: "hospital-management/hospitals",
};

const API = {
  hospitals: (params) => api.get(apis.hospitals, { params }),
  hospital: (id) => api.get(apis.hospital(id)),
};

export default API;
