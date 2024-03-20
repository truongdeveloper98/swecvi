/* eslint-disable no-console */
import PAGES from "navigation/pages";
import { useRef, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { failed } from "stores/reducers/patient.reducer";
import {
  deletePatientRequest,
  examPatientsRequest,
  exportPatientRequest,
  forceSyncPatientsRequest,
  getStudiesByPatientIdRequest,
} from "../services";

const usePatients = () => {
  const navigate = useNavigate();
  const period = useSelector((state) => state.common.period);
  const agRef = useRef(null);
  const [isExport, setIsExport] = useState(false);
  const [valueExport, setValueExport] = useState({ startDate: "", endDate: "", patient: null });

  const [targetPatientId, setTargetPatientId] = useState(undefined);
  const dispatch = useDispatch();

  const handleOpenExamDetail = (hospitalId, id) => {
    if (!id) return;
    navigate(`${PAGES.editExam}/${hospitalId}/${id}`);
  };

  const handleDeletePatient = (id, cb) => {
    if (!id) return;
    deletePatientRequest(id, cb);
  };

  const handleSelectPatient = (patient, callback) => {
    if (!patient) return;
    // console.log("Patient %d", patient);
    setTargetPatientId(patient.patientId);
    getStudiesByPatientIdRequest(patient.hospitalId, patient.patientId, { period }, callback);
  };

  const handleForceSyncPatients = () => {
    forceSyncPatientsRequest(() => {
      agRef.current?.fetchData();
      examPatientsRequest();
    });
  };

  const handleSave = async () => {
    if (valueExport.startDate > valueExport.endDate) {
      dispatch(failed("Thời gian bắt đầu nhỏ hơn thời gian kết thúc"));
    } else {
      // console.log(valueExport);
      await exportPatientRequest({
        startDate: valueExport.startDate || null,
        endDate: valueExport.endDate || null,
        patientId: valueExport.patient ? valueExport.patient.patientId : null,
        studyId: null,
      });
    }
  };

  return {
    agRef,

    targetPatientId,
    isExport,
    valueExport,
    setIsExport,
    handleSave,
    setValueExport,
    // funcs
    handleSelectPatient,
    handleOpenExamDetail,
    handleDeletePatient,
    handleForceSyncPatients,
  };
};
export default usePatients;
