import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useSelector } from "react-redux";
import API from "features/patient-finding/api";
import { getValueInObject } from "utils/convertData";
import { store } from "stores";
import { failed, succeed } from "stores/reducers/patient.reducer";
import { getPatientFindingBox } from "../services";

function usePatientFinding() {
  const patientFindingValueInput = useSelector((state) => state.patient.patientFindingValueInput);
  // const patientCreateOrUpdate = useSelector((state) => state.patient.patientCreateOrUpdate);
  const [openOverlay, setOpenOverlay] = useState(false);

  const params = useParams();

  const { dispatch } = store;

  const handleBtnSave = async () => {
    try {
      setOpenOverlay(true);
      await API.studyFinding({
        HospitalId: params.hospitalId,
        StudyId: params.id,
        FingdingStudyItems: getValueInObject(patientFindingValueInput),
      });
      setOpenOverlay(false);
      dispatch(succeed("Study Finding created successfully"));
    } catch (error) {
      dispatch(failed(error?.response?.data));
    }
  };

  useEffect(() => {
    getPatientFindingBox(params, patientFindingValueInput);
  }, []);

  return {
    params,
    handleBtnSave,
    openOverlay,
  };
}

export default usePatientFinding;
