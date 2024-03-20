import { store } from "stores";
import {
  requested,
  failed,
  succeed,
  sessionsSuccess,
  sessionsFieldSuccess,
} from "stores/reducers/session.reducer";
import API from "./api";

export const getSessionsRequest = async () => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const request = await API.sessions();
    if (request.data) {
      dispatch(sessionsSuccess({ items: request.data }));
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const createSessionRequest = async (params, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.createSession(params);
    dispatch(succeed("Session created successfully"));
    if (callback) {
      callback();
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const deleteSessionRequest = async (sessionId, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.deleteSession(sessionId);
    dispatch(succeed("Session deleted successfully"));
    if (callback) callback();
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const updateSessionRequest = async (sessionId, params, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.updateSession(sessionId, params);
    dispatch(succeed("Session updated successfully"));
    if (callback) callback();
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const getSessionFieldRequest = async (sessionId, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const request = await API.sessionFieldsBySession(sessionId);
    if (request.data) {
      dispatch(sessionsFieldSuccess(request.data));
      callback(request.data);
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const createSessionFieldRequest = async (params, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.createSessionField(params);
    dispatch(succeed("Session field created successfully"));
    if (callback) {
      callback();
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const updateSessionFieldRequest = async (sessionFieldId, params, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.updateSessionField(sessionFieldId, params);
    dispatch(succeed("Session field updated successfully"));
    if (callback) {
      callback();
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const deleteSessionFieldRequest = async (sessionFieldId, callback) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    await API.deleteSessionField(sessionFieldId);
    dispatch(succeed("Session field deleted successfully!"));
    if (callback) {
      callback();
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};
