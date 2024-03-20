import api from "services/api";

const apis = {
  pythons: "python-management/python-codes",
  python: (pythonId) => `python-management/python-codes/${pythonId}`,
  pythonCurrent: (pythonId) => `python-management/python-codes/${pythonId}/current-version`,
  resetDefault: (pythonId) => `python-management/python-codes/${pythonId}/reset-default`,
};

const API = {
  pythons: () => api.get(apis.pythons),
  deletePython: (id) => api.delete(apis.python(id)),
  pythonCurrent: (id) => api.put(apis.pythonCurrent(id)),
  resetDefault: (id) => api.put(apis.resetDefault(id)),
  createPython: (id, params) => api.post(apis.python(id), params),
};

export default API;
