import api from "services/api";

const apis = {
  user: (userId) => `user-management/users/${userId}`,

  users: "user-management/users",
};

const API = {
  updateUser: (id, data) => api.put(apis.user(id), data),

  createUser: (data) => api.post(apis.users, data),

  user: (id) => api.get(apis.user(id)),
};

export default API;
