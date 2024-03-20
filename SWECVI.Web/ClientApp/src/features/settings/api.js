import api from "services/api";

const apis = {
  settingss: "parametersetting-management/parametersettings",
};

const API = {
  settingss: (params) => api.get(apis.settingss, { params }),
};

export default API;
