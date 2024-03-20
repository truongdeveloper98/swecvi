import { useRef, useState } from "react";
import {
  createSessionFieldRequest,
  createSessionRequest,
  deleteSessionFieldRequest,
  deleteSessionRequest,
  getSessionsRequest,
  updateSessionFieldRequest,
  updateSessionRequest,
} from "../services";

const useSessions = () => {
  const dialogRef = useRef(null);
  const [targetSession, setTargetSession] = useState();

  const handleOpenSessionModal = (data) => {
    dialogRef.current?.setEntity("Section");
    if (data?.id) {
      dialogRef.current?.setInit(data);
    }
    dialogRef.current?.handleOpen();
  };

  const handleOpenSessionFieldModal = (data) => {
    dialogRef.current?.setEntity("Section Field");
    if (data?.id) {
      dialogRef.current?.setInit(data);
    }
    dialogRef.current?.handleOpen();
  };

  const handleSelectSession = (data) => {
    if (!data) return;
    setTargetSession(data);
  };

  // session functions
  const createSession = (data) => {
    createSessionRequest(data, getSessionsRequest);
  };

  const updateSession = (id, params) => {
    if (!params.name) return;
    updateSessionRequest(id, params, getSessionsRequest);
  };

  const handleDeleteSession = (id) => {
    deleteSessionRequest(id, getSessionsRequest);
  };

  // session field functions
  const createSessionField = (data) => {
    if (!targetSession) return;
    data.sessionId = targetSession.id;
    createSessionFieldRequest(data, getSessionsRequest);
  };

  const updateSessionField = (id, data) => {
    if (!targetSession) return;
    if (!data.name) return;
    data.sessionId = targetSession.id;
    updateSessionFieldRequest(id, data, getSessionsRequest);
  };

  const handleDeleteSessionField = (id) => {
    deleteSessionFieldRequest(id, getSessionsRequest);
  };

  const handleCreate = (data) => {
    if (dialogRef.current?.entity() === "Section") {
      createSession(data);
    } else {
      createSessionField(data);
    }
  };

  const handleUpdate = (id, data) => {
    if (dialogRef.current?.entity() === "Section") {
      updateSession(id, data);
    } else {
      updateSessionField(id, data);
    }
  };

  return {
    // states
    dialogRef,

    // funcs
    handleSelectSession,

    // sessions
    handleDeleteSession,

    // fields
    handleDeleteSessionField,

    // modal
    handleOpenSessionModal,
    handleOpenSessionFieldModal,

    handleCreate,
    handleUpdate,
  };
};

export default useSessions;
