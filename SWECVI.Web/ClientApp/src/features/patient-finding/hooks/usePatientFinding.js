import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { useSelector } from "react-redux";
import { getPatientFindingBox } from "../services";

const usePatientFinding = () => {
  const params = useParams();
  const patientFindingValueInput = useSelector((state) => state.patient.patientFindingValueInput);

  useEffect(() => {
    getPatientFindingBox(params, patientFindingValueInput);
  }, []);

  return {
    params,
  };
};

export default usePatientFinding;
