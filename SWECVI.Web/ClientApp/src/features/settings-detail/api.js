import api from "services/api";

const apis = {
  settings: (id) => `parametersetting-management/parametersettings/${id}`,
  settingss: "parametersetting-management/parametersettings",
};

const API = {
  updateSettings: (id, data) => api.put(apis.settings(id), data),
  createSettings: (data) => api.post(apis.settingss, data),
  settings: (id) => api.get(apis.settings(id)),
};

export default API;
