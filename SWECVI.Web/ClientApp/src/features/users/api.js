import api from "services/api";

const apis = {
  activeUser: (userId) => `user-management/users/${userId}/active`,

  inactiveUser: (userId) => `user-management/users/${userId}/inactive`,

  users: "user-management/users",
};

const API = {
  activeUser: (id) => api.post(apis.activeUser(id)),

  inactiveUser: (id) => api.post(apis.inactiveUser(id)),

  users: (params) => api.get(apis.users, { params }),
};

export default API;
