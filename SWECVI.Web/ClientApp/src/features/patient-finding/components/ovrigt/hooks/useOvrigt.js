import API from "features/patient-finding/api";
import { updateStudyFinding } from "features/patient-finding/services";
import { useState } from "react";
import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import { getValueInObject } from "utils/convertData";

const useOvrigt = () => {
  const patientFindingValueInput = useSelector((state) => state.patient.patientFindingValueInput);
  // const patientCreateOrUpdate = useSelector((state) => state.patient.patientCreateOrUpdate);
  const params = useParams();

  const [openOverlay, setOpenOverlay] = useState(false);
  const handleBtnSave = async () => {
    // console.log(patientFindingValueInput);
    // console.log(patientCreateOrUpdate);
    setOpenOverlay(true);
    await API.studyFinding({
      HospitalId: params.hospitalId,
      StudyId: params.id,
      FingdingStudyItems: getValueInObject(patientFindingValueInput),
    });
    setOpenOverlay(false);
  };

  const handleBtnUpdate = async () => {
    setOpenOverlay(true);
    await updateStudyFinding(params, getValueInObject(patientFindingValueInput));
    setOpenOverlay(false);
  };

  return {
    handleBtnSave,
    handleBtnUpdate,
    openOverlay,
  };
};

export default useOvrigt;
