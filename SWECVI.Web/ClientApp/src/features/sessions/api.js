import api from "services/api";

const apis = {
  sessions: "session-management/sessions?includes=fields",

  session: (sessionId) => `session-management/sessions/${sessionId}`,

  sessionFieldsBySession: (sessionId) => `session-management/sessions/${sessionId}/fields`,

  sessionFields: "field-management/fields",

  sessionField: (sessionFieldId) => `field-management/fields/${sessionFieldId}`,
};

const API = {
  // sessions
  sessions: () => api.get(apis.sessions),

  createSession: (params) => api.post(apis.sessions, params),

  updateSession: (id, params) => api.put(apis.session(id), params),

  deleteSession: (id) => api.delete(apis.session(id)),

  // session fields
  sessionFieldsBySession: (id) => api.get(apis.sessionFieldsBySession(id)),

  createSessionField: (params) => api.post(apis.sessionFields, params),

  updateSessionField: (id, params) => api.put(apis.sessionField(id), params),

  deleteSessionField: (id) => api.delete(API.sessionField(id)),
};

export default API;
